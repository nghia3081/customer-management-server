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
        public override Task<Customer> Create(Customer businessObject)
        {

            businessObject.CreatedBy = base.GetLoggedInUser().Username;
            return base.Create(businessObject);
        }
        [HttpGet("get-with-custom-response")]
        public override IActionResult GetWithCustomResponse(ODataQueryOptions<Repository.Entities.Customer> odataOptions)
        {
            var user = GetLoggedInUser();
            string username = user.Username;
            var customerSet = this.repository.Entities.Where(c => user.IsAdmin || c.CreatedBy.Equals(username));
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
        [HttpGet("get-number-customers")]
        public async Task<IEnumerable<Report<long>>> GetNumberCustomers(int? year = null)
        {
            var user = this.GetLoggedInUser();
            var result = await (repository as ICustomerRepository).GetNumberCustomerReportsAsync(user, year);

            return result;
        }
    }
}
