using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

namespace Api.Controllers
{
    public class PackageController : GenericController<String, BusinessObject.Models.Package, Repository.Entities.Package>
    {
        public PackageController(IPackageRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
