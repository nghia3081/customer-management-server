using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.IServices;
using Repository.Repositories.Base;
using System.Net.Mail;

namespace Repository.Repositories
{
    internal class ContractRepository : GenericRepository<BusinessObject.Models.Contract, Entities.Contract>, IContractRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmailService emailService;
        public ContractRepository(CustomerManageContext context,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IEmailService emailService) : base(context, mapper)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.emailService = emailService;
        }

        public async Task<BusinessObject.Models.Contract> ActiveLicense(Guid contractId, DateTime licenseStartDate)
        {
            var contract = await this.Find(contractId);
            bool isValidStartDate = IsValidStartDate(contract.CustomerId, licenseStartDate);
            if (!isValidStartDate)
            {
                throw new CustomerManagementException(4002);
            }
            contract.LicenseStartDate = licenseStartDate;
            contract.LicenseEndDate = licenseStartDate.AddMonths(contract.Details.Sum(c => c.LicenseMonthQuantity));
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }
        private bool IsValidStartDate(Guid customerId, DateTime licenseStartDate)
        {
            DateTime? endLicenseOfCustomer = this.context.Contracts.Where(c => c.CustomerId == customerId).Max(c => c.LicenseEndDate);
            if (endLicenseOfCustomer is null) return true;
            return licenseStartDate >= endLicenseOfCustomer;

        }
        public override async Task<BusinessObject.Models.Contract> Create(BusinessObject.Models.Contract businessObject)
        {
            var customer = this.context.Customers.Find(businessObject.CustomerId);
            this.context.Entry(customer).Navigation("Contracts").Load();
            businessObject.Code = $"{customer?.TaxCode}_CONTRACT_{customer?.Contracts.Count()}";
            return await base.Create(businessObject);
        }

        public async Task<BusinessObject.Models.Contract> SendToApprove(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.StatusId > 1)
                throw new CustomerManagementException(4001);
            contract.StatusId = 2;
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public override async Task<BusinessObject.Models.Contract> Update(BusinessObject.Models.Contract businessObject)
        {
            var detail = this.context.ContractDetails.Where(d => d.ContractId == businessObject.Id);
            this.context.RemoveRange(detail);
            this.context.SaveChanges();
            return await base.Update(businessObject);
        }
        public override async Task<BusinessObject.Models.Contract> Delete(params object[] objectKeys)
        {
            var contract = await this.Find(objectKeys);
            if (contract.StatusId > 1 || contract.LicenseStartDate != null || contract.InvoiceExportedDate != null)
                throw new CustomerManagementException(4003);
            this.entities.Remove(contract);
            await this.context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public async Task<BusinessObject.Models.Contract> ExportInvoice(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.InvoiceExportedDate != null)
                throw new CustomerManagementException(4005);
            contract.InvoiceExportedDate = DateTime.Now;
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public async Task<BusinessObject.Models.Contract> Approve(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.StatusId != 2)
                throw new CustomerManagementException(4006);
            if(contract.LicenseStartDate == null)
            {
                throw new CustomerManagementException(4015);
            }
            contract.StatusId = 3;
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public async Task<BusinessObject.Models.Contract> Reject(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.StatusId != 2)
                throw new CustomerManagementException(4006);
            contract.StatusId = 6;
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public async Task SendToCustomer(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.StatusId < 4 || contract.StatusId == 6)
            {
                throw new CustomerManagementException(4009);
            }
            var contractFile = await this.PrintPdf(contractId);
            string path = Path.Combine(webHostEnvironment.WebRootPath, "templates/email/send-contract-to-customer.html");
            var contractMailContent = File.ReadAllText(path);
            contractMailContent = contractMailContent.Replace("{customerName}", contract.Customer.Name);
            MailMessage mailMessage = emailService.CreateMailMessage("Get your contract", contractMailContent, true, contract.Customer.Email);
            mailMessage = emailService.AttachFile(mailMessage, contractFile);
            await emailService.SendMessage(mailMessage);
            contract.StatusId = 5;
            this.entities.Update(contract);
            await this.context.SaveChangesAsync();
        }

        public async Task<byte[]> PrintPdf(Guid contractId)
        {
            var contract = await this.Find(contractId);
            string path = Path.Combine(webHostEnvironment.WebRootPath, $"contracts\\{contractId}.pdf");
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            string templatePath = Path.Combine(webHostEnvironment.WebRootPath, "templates\\contract\\template.html");
            var data = await FilePrinting.PrintContract(templatePath, mapper.Map<BusinessObject.Models.Contract>(contract));
            // File.WriteAllBytes(path, data);
            return data;
        }

        public async Task<byte[]> SignPdf(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.StatusId < 3 || contract.StatusId == 6)
            {
                throw new CustomerManagementException(4014);
            }
            if (contract.IsSigned)
                throw new CustomerManagementException(4007);
            string contractFilePath = Path.Combine(webHostEnvironment.WebRootPath, $"contracts\\{contractId}.pdf");
            byte[] contractFileByte;
            if (File.Exists(contractFilePath))
            {
                contractFileByte = File.ReadAllBytes(contractFilePath);
            }
            else
            {
                contractFileByte = await PrintPdf(contractId);
            }
            string certPath = webHostEnvironment.WebRootPath + "/signatures/cert-m.p12";
            string certLogoPath = webHostEnvironment.WebRootPath + "/images/logo/logo.png";
            var data = await FilePrinting.SignPdf(contractFileByte, certPath, "2e0bBi7G", certLogoPath);
            contract.IsSigned = true;
            contract.StatusId = 4;
            this.entities.Update(contract);
            await this.context.SaveChangesAsync();
            await File.WriteAllBytesAsync(contractFilePath, data);
            return data;
        }

        public async Task<IEnumerable<Report<long>>> GetNumberContractsReports(BusinessObject.Models.User user, int? year = null)
        {
            year ??= DateTime.Now.Year;
            int month = 1;
            IEnumerable<Report<long>> reports = Enumerable.Empty<Report<long>>();
            while (month <= 12)
            {
                var dataInMonth = await this.entities
                    .Where(c => c.CreatedDate.Year == year
                            && (user.IsAdmin || c.CreatedBy.Equals(user.Username))
                            && c.CreatedDate.Month == month
                            && c.IsSigned
                            )
                    .CountAsync();
                reports = reports.Append(new Report<long> { Month = month, MonthValue = dataInMonth });
                month++;
            }
            return reports;
        }

        public async Task<IEnumerable<Report<decimal>>> GetIncomeValueReports(BusinessObject.Models.User user, int? year = null)
        {
            year ??= DateTime.Now.Year;
            int month = 1;
            IEnumerable<Report<decimal>> reports = Enumerable.Empty<Report<decimal>>();
            while (month <= 12)
            {
                var detail = await this.context.ContractDetails
                    .Where(c => c.Contract.CreatedDate.Year == year
                    && (user.IsAdmin || c.Contract.CreatedBy.Equals(user.Username))
                    && c.Contract.CreatedDate.Month == month
                     && c.Contract.IsSigned)
                    .SumAsync(dt => (dt.UnitPrice * dt.Quantity) - (dt.UnitPrice * dt.Quantity * (decimal)dt.Discount / 100) - (dt.UnitPrice * dt.Quantity * dt.TaxValue / 100));
                reports = reports.Append(new Report<decimal> { Month = month, MonthValue = detail });
                month++;
            }
            return reports;
        }
    }
}
