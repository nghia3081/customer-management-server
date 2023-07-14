using AutoMapper;
using Repository.Entities;
using Repository.IRepositories;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class MenuRepository : GenericRepository<BusinessObject.Models.Menu, Entities.Menu>, IMenuRepository
    {
        public MenuRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
