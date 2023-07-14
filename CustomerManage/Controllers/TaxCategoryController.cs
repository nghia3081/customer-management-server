using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using Repository.Entities;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class TaxCategoryController : GenericController<Guid, BusinessObject.Models.TaxCategory, Repository.Entities.TaxCategory>
    {
        public TaxCategoryController(ICategoryRepository<Repository.Entities.TaxCategory, BusinessObject.Models.TaxCategory> repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
        [HttpGet("get-options")]
        public IActionResult GetOptions(ODataQueryOptions<TaxCategory> odataOptions)
        {
            var data = odataOptions.ApplyTo(repository.Entities.AsQueryable());
            var odataFeature = HttpContext.ODataFeature();
            return Ok(data.OfType<TaxCategory>().Select(c => new { c.Id, c.Value }));
        }
    }
}
