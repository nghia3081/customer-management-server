using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IMenuRepository : IGenericRepository<BusinessObject.Models.Menu, Entities.Menu>
    {
    }
}
