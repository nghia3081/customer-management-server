using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository.IRepositories;
using Repository.IRepositories.Base;

namespace Api.Controllers
{
    public class UserController : GenericController<string, BusinessObject.Models.User, Repository.Entities.User>
    {
        public UserController(IUserRepository repository, IMapper mapper, IOptions<AppSetting> options) : base(repository, mapper, options)
        {
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginResponse> Login(BusinessObject.Models.LoginRequest loginDto)
        {
            var (token, expire) = await (repository as IUserRepository).Login(loginDto.UserName, loginDto.Password);
            return new()
            {
                Token = token,
                Expire = expire,
            };
        }
        [HttpPost]
        [AllowAnonymous]
        public override async Task<User> Create(User businessObject)
        {
            return await base.Create(businessObject);
        }
    }
}
