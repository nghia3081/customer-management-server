using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.Extensions.Options;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class StatusCategoryController : GenericController<Guid, BusinessObject.Models.StatusCategory, Repository.Entities.StatusCategory>
    {
        public StatusCategoryController(ICategoryRepository<Repository.Entities.StatusCategory, BusinessObject.Models.StatusCategory> repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
