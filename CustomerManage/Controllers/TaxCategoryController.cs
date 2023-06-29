using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.Extensions.Options;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class TaxCategoryController : GenericController<Guid, BusinessObject.Models.TaxCategory, Repository.Entities.TaxCategory>
    {
        public TaxCategoryController(ICategoryRepository<Repository.Entities.TaxCategory, BusinessObject.Models.TaxCategory> repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
