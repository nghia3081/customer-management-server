using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

namespace Api.Controllers
{
    public class CustomerController : GenericController<Guid, BusinessObject.Models.Customer, Repository.Entities.Customer>
    {
        public CustomerController(ICustomerRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
        [HttpPost]
        public override Task<Customer> Create(Customer businessObject)
        {

            businessObject.CreatedBy = base.GetLoggedInUsername();
            return base.Create(businessObject);
        }
        [HttpGet("get-with-custom-response")]
        public override IActionResult GetWithCustomResponse(ODataQueryOptions<Repository.Entities.Customer> odataOptions)
        {
            string username = GetLoggedInUsername();
            var customerSet = this.repository.Entities.Where(c => c.CreatedBy.Equals(username));
            var data = odataOptions.ApplyTo(customerSet);
            var odataFeature = HttpContext.ODataFeature();
            var response = new ClientOdataResponseFormat<Repository.Entities.Customer>()
            {
                Pos = odataOptions.Skip == null ? 0 : odataOptions.Skip.Value,
                Total = odataFeature.TotalCount ?? 0,
                Data = data,
            };
            return Ok(response);
        }
    }
}
