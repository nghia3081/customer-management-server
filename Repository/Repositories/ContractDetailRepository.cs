using AutoMapper;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.Base;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class ContractDetailRepository : GenericRepository<BusinessObject.Models.ContractDetail, Entities.ContractDetail>, IContractDetailRepository
    {
        public ContractDetailRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
