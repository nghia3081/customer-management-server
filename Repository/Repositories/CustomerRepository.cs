using AutoMapper;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.Base;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class CustomerRepository : GenericRepository<BusinessObject.Models.Customer, Entities.Customer>,ICustomerRepository
    {
        public CustomerRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
