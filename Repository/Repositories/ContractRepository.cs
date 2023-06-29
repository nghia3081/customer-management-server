using AutoMapper;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.Base;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class ContractRepository : GenericRepository<BusinessObject.Models.Contract, Entities.Contract>, IContractRepository
    {
        public ContractRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
