using AutoMapper;
using Repository.Entities;
using Repository.IRepositories.Base;

namespace Repository.Repositories.Base
{
    internal class CategoryRepository<TCategoryEntity, TBusinessObject> : GenericRepository<TBusinessObject, TCategoryEntity>, ICategoryRepository<TCategoryEntity, TBusinessObject>
        where TCategoryEntity : class
        where TBusinessObject : class
    {
        public CategoryRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
