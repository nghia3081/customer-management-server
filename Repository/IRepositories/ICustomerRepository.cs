using BusinessObject.Models;
using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<BusinessObject.Models.Customer, Entities.Customer>
    {
        Task<IEnumerable<Report<long>>> GetNumberCustomerReportsAsync(BusinessObject.Models.User user, int? year = null);
    }
}
