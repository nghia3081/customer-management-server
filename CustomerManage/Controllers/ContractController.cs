using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.Extensions.Options;
using Repository.IRepositories;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class ContractController : GenericController<Guid, BusinessObject.Models.Contract, Repository.Entities.Contract>
    {
        public ContractController(IContractRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
