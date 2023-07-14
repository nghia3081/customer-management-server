using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IContractRepository : IGenericRepository<BusinessObject.Models.Contract, Entities.Contract>
    {
        public Task<BusinessObject.Models.Contract> SendToApprove(Guid contractId);
        public Task<BusinessObject.Models.Contract> ExportInvoice(Guid contractId);
        public Task<BusinessObject.Models.Contract> Approve(Guid contractId);
        public Task<BusinessObject.Models.Contract> Reject(Guid contractId);
        public Task<bool> SendToCustomer(Guid contractId);
        public Task<byte[]> PrintPdf(Guid contractId);
        public Task<byte[]> SignPdf(Guid contractId);
        public Task<BusinessObject.Models.Contract> ActiveLicense(Guid contractId, DateTime licenseStartDate);
    }
}
