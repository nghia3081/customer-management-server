using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IPackageRepository : IGenericRepository<BusinessObject.Models.Package, Entities.Package>
    {
    }
}
