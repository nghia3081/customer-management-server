using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class StatusCategoryController : GenericController<Guid, BusinessObject.Models.StatusCategory, Repository.Entities.StatusCategory>
    {
        public StatusCategoryController(ICategoryRepository<Repository.Entities.StatusCategory, BusinessObject.Models.StatusCategory> repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
        [HttpGet("get-options")]
        public IActionResult GetOptions(ODataQueryOptions<Repository.Entities.StatusCategory> odataOptions)
        {
            var data = odataOptions.ApplyTo(repository.Entities.AsQueryable());
            var odataFeature = HttpContext.ODataFeature();
            return Ok(data.OfType<Repository.Entities.StatusCategory>().Select(c => new { c.Id, c.Value }));
        }
    }
}
