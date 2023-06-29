using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.Extensions.Options;
using Repository.IRepositories;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class CustomerController : GenericController<Guid, BusinessObject.Models.Customer, Repository.Entities.Customer>
    {
        public CustomerController(ICustomerRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
