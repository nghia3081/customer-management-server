namespace Repository.IRepositories.Base
{
    public interface ICategoryRepository<TCategoryEntity, TBusinessObject>  : IGenericRepository<TBusinessObject, TCategoryEntity>
        where TCategoryEntity : class
        where TBusinessObject : class
    {
    }
}
