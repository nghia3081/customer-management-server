using AutoMapper;
using BusinessObject;
using Repository.Entities;
using Repository.IRepositories;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class CustomerRepository : GenericRepository<BusinessObject.Models.Customer, Entities.Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override async Task<BusinessObject.Models.Customer> Delete(params object[] objectKeys)
        {
            var customer = await this.Find(objectKeys);
            this.context.Entry(customer).Navigation("Contracts");
            if (customer.Contracts.Any())
                throw new CustomerManagementException(4004);
            this.Entities.Remove(customer);
            this.context.SaveChanges();
            return mapper.Map<BusinessObject.Models.Customer>(customer);
        }
    }
}
