using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<BusinessObject.Models.Customer, Entities.Customer>
    {
    }
}
