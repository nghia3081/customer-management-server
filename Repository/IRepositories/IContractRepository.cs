using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IContractRepository : IGenericRepository<BusinessObject.Models.Contract, Entities.Contract>
    {
    }
}
