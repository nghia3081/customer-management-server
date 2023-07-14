using AutoMapper;
using BusinessObject;
using CusomterManager.Controllers.Base;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

namespace Api.Controllers
{
    public class ContractDetailController : GenericController<Guid, BusinessObject.Models.ContractDetail, Repository.Entities.ContractDetail>
    {
        public ContractDetailController(IContractDetailRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
    }
}
