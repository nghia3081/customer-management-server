using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

namespace Api.Controllers
{
    public class MenuController : GenericController<string, BusinessObject.Models.Menu, Repository.Entities.Menu>
    {
        public MenuController(IMenuRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
