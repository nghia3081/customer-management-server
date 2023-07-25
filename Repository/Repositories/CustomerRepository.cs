using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Report<long>>> GetNumberCustomerReportsAsync(BusinessObject.Models.User user, int? year = null)
        {
            year ??= DateTime.Now.Year;
            int month = 1;
            IEnumerable<Report<long>> reports = Enumerable.Empty<Report<long>>();
            while (month <= 12)
            {
                var dataInMonth = await this.entities
                    .Where(c => c.CreatedDate.Year == year
                    && (user.IsAdmin || c.CreatedBy == user.Username)
                            && c.CreatedDate.Month == month
                            )
                    .CountAsync();
                reports = reports.Append(new Report<long> { Month = month, MonthValue = dataInMonth });
                month++;
            }
            return reports;
        }
    }
}
