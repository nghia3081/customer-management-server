using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IContractDetailRepository : IGenericRepository<BusinessObject.Models.ContractDetail, Entities.ContractDetail>
    {
    }
}
