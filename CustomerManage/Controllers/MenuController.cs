using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

namespace Api.Controllers
{
    public class MenuController : GenericController<string, BusinessObject.Models.Menu, Repository.Entities.Menu>
    {
        public MenuController(IMenuRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
        public override IActionResult GetWithCustomResponse(ODataQueryOptions<Repository.Entities.Menu> odataOptions)
        {
            var data = odataOptions.ApplyTo(repository.Entities.Include(m => m.ChildMenus).AsQueryable());
            var odataFeature = HttpContext.ODataFeature();
            var response = new ClientOdataResponseFormat<Repository.Entities.Menu>()
            {
                Pos = odataOptions.Skip == null ? 0 : odataOptions.Skip.Value,
                Total = odataFeature.TotalCount ?? 0,
                Data = data,
            };
            return Ok(response);
        }
    }
}
