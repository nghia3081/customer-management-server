using AutoMapper;
using BusinessObject;
using Repository.Entities;
using Repository.IRepositories;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class ContractRepository : GenericRepository<BusinessObject.Models.Contract, Entities.Contract>, IContractRepository
    {
        public ContractRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
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
            if (contract.StatusId < 2)
                throw new CustomerManagementException(4006);
            contract.StatusId = 3;
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public async Task<BusinessObject.Models.Contract> Reject(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.StatusId < 2)
                throw new CustomerManagementException(4006);
            contract.StatusId = 6;
            entities.Update(contract);
            await context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.Contract>(contract);
        }

        public Task<bool> SendToCustomer(Guid contractId)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> PrintPdf(Guid contractId)
        {
            var contract = await this.Find(contractId);
            string contractFilePath = $"contracts\\printed\\{contractId}.pdf";
            if (File.Exists(contractFilePath))
            {
                return File.ReadAllBytes(contractFilePath);
            }
            string contractFileTemplate = $"contracts\\template\\{contractId}.repx";
            var data = await FilePrinting.Print(contract, contractFileTemplate);
            File.WriteAllBytes(contractFilePath, data);
            return data;
        }

        public async Task<byte[]> SignPdf(Guid contractId)
        {
            var contract = await this.Find(contractId);
            if (contract.IsSigned)
                throw new CustomerManagementException(4007);
            string contractFilePath = $"contracts\\printed\\{contractId}.pdf";
            byte[] contractFileByte;
            if (File.Exists(contractFilePath))
            {
                contractFileByte = File.ReadAllBytes(contractFilePath);
            }
            else
            {
                contractFileByte = await PrintPdf(contractId);
            }
            var data = await FilePrinting.SignPdf(contractFileByte, "certificate-other.pfx", "nghia", mapper.Map<BusinessObject.Models.Contract>(contract));
            contract.IsSigned = true;
            this.entities.Update(contract);
            await this.context.SaveChangesAsync();
            return data;
        }
    }
}
