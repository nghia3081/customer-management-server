using AutoMapper;
using Repository.Entities;
using Repository.IRepositories;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    internal class PackageRepository : GenericRepository<BusinessObject.Models.Package, Entities.Package>, IPackageRepository
    {
        public PackageRepository(CustomerManageContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
