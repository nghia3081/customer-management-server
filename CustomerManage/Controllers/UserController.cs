using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using CusomterManager.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository.IRepositories;

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
            return await (repository as IUserRepository).Login(loginDto.UserName, loginDto.Password);
        }
        [HttpPost]
        [AllowAnonymous]
        public override async Task<User> Create(User businessObject)
        {
            return await base.Create(businessObject);
        }
        [HttpPut("update-information")]
        public async Task<User> UpdateInformation(UpdateUserInformationDto user)
        {
            return await (repository as IUserRepository).UpdateInformation(user);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            changePasswordDto.UserName = base.GetLoggedInUser().Username;
            _ = await (repository as IUserRepository).ChangePassword(changePasswordDto);
            return NoContent();
        }
        [HttpPatch("{username}/block")]
        public async Task<BusinessObject.Models.User> Block(string username)
        {
            return await (repository as IUserRepository).Block(username);
        }
        [HttpPut("permission")]
        public async Task<BusinessObject.Models.User> Permission([FromBody] BusinessObject.Models.UpdateUserPermissionDto user)
        {
            return await (repository as IUserRepository).Permission(user);
        }
        [HttpPatch("reset-password")]
        [AllowAnonymous]
        public async Task<bool> ResetPassword(BusinessObject.Models.ResetPasswordDto resetPasswordDto)
        {
            return await (repository as IUserRepository).ResetPassword(resetPasswordDto);
        }

    }
}
