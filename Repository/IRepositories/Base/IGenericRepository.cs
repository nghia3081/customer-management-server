using Microsoft.EntityFrameworkCore;

namespace Repository.IRepositories.Base
{
    public interface IGenericRepository<TBusinessObject, TEntity>
        where TBusinessObject : class
        where TEntity : class
    {
        DbSet<TEntity> Entities { get; }
        Task<IEnumerable<TBusinessObject>> GetAll();
        Task<TBusinessObject> Create(TBusinessObject businessObject);
        Task<TBusinessObject> Update(TBusinessObject businessObject);
        Task<TBusinessObject> Delete(params object[] objectKeys);
        Task<TBusinessObject> FindMapping(params object[] objectKeys);
        Task<TEntity> Find(params object[] objectKeys);
    }
}
