using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

namespace Api.Controllers
{
    public class ContractController : GenericController<Guid, BusinessObject.Models.Contract, Repository.Entities.Contract>
    {
        private IContractRepository contractRepository;
        public ContractController(IContractRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
            this.contractRepository = repository;
        }
        public override Task<Contract> Create(Contract businessObject)
        {
            businessObject.CreatedBy = this.GetLoggedInUser().Username;
            return base.Create(businessObject);
        }
        [HttpPatch("{id}/send-to-approve")]
        public async Task<BusinessObject.Models.Contract> SendToApprove(Guid id)
        {
            return await contractRepository.SendToApprove(id);
        }
        [HttpPatch("{id}/export-invoice")]
        public async Task<BusinessObject.Models.Contract> ExportInvoice(Guid id)
        {
            return await contractRepository.ExportInvoice(id);
        }
        [HttpPatch("{id}/active-license")]
        public async Task<BusinessObject.Models.Contract> ActiveLicense(Guid id, [FromBody] DateTime licenseStartDate)
        {
            return await contractRepository.ActiveLicense(id, licenseStartDate);
        }
        [HttpPatch("{id}/approve")]
        public async Task<BusinessObject.Models.Contract> Approve(Guid id)
        {
            return await contractRepository.Approve(id);
        }
        [HttpPatch("{id}/reject")]
        public async Task<BusinessObject.Models.Contract> Reject(Guid id)
        {
            return await contractRepository.Reject(id);
        }
        [HttpGet("{id}/print")]
        public async Task<IActionResult> Print(Guid id)
        {
            var data = await contractRepository.PrintPdf(id);
            return File(data, "application/pdf");
        }
        [HttpPatch("{id}/sign")]
        public async Task<IActionResult> Sign(Guid id)
        {
            var data = await contractRepository.SignPdf(id);
            return NoContent();
        }
        [HttpPatch("{id}/send-to-customer")]
        public async Task SendToCustomer(Guid id)
        {
            await contractRepository.SendToCustomer(id);
        }
        [HttpGet("get-number-contracts")]
        public async Task<IEnumerable<Report<long>>> GetNumberContracts(int? year = null)
        {
            var user = this.GetLoggedInUser();
            var result = await (repository as IContractRepository).GetNumberContractsReports(user, year);

            return result;
        }
        [HttpGet("get-income-value")]
        public async Task<IEnumerable<Report<decimal>>> GetIncomeValue(int? year = null)
        {
            var user = this.GetLoggedInUser();
            var result = await (repository as IContractRepository).GetIncomeValueReports(user, year);

            return result;
        }
    }
}
