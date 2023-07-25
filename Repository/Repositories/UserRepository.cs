using AutoMapper;
using BusinessObject;
using BusinessObject.Models;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.IServices;
using Repository.Repositories.Base;
using Repository.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Repository.Repositories
{
    internal class UserRepository : GenericRepository<BusinessObject.Models.User, Entities.User>, IUserRepository
    {
        private readonly AppSetting appSetting;
        private readonly IEmailService emailService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserRepository(CustomerManageContext context,
            IMapper mapper,
            IOptions<AppSetting> options,
            IEmailService emailService,
            IWebHostEnvironment webHostEnvironment) : base(context, mapper)
        {
            this.appSetting = options.Value;
            this.emailService = emailService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<BusinessObject.Models.LoginResponse> Login(string username, string password)
        {

            Repository.Entities.User user = await Find(username);
            if (user.IsBlocked)
            {
                throw new CustomerManagementException(4013);
            }
            var hashService = new HashServices(password, appSetting.HashSalt);
            if (hashService.IsPassed(user.Password))
            {
                var response = mapper.Map<BusinessObject.Models.LoginResponse>(user);
                var (token, expires) = HashServices.GenerateJwtToken(appSetting.Jwt, user);
                response.Token = token;
                response.Expire = expires;
                return response;
            }
            throw new CustomerManagementException(4010);

        }
        public override Task<BusinessObject.Models.User> Create(BusinessObject.Models.User user)
        {
            HashServices hashServices = new(user.Password, appSetting.HashSalt);
            user.Password = hashServices.EncryptedPassword;
            return base.Create(user);
        }
        public override async Task<BusinessObject.Models.User> Delete(params object[] objectKeys)
        {
            var rawUser = await this.Find(objectKeys);
            if (rawUser.IsAdmin)
            {
                throw new CustomerManagementException(4012);
            }
            this.entities.Remove(rawUser);
            await this.context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.User>(rawUser);
        }
        public async Task<BusinessObject.Models.User> UpdateInformation(BusinessObject.Models.UpdateUserInformationDto user)
        {
            var rawUser = await this.Find(user.Username);
            rawUser.Phone = user.Phone;
            rawUser.Email = user.Email;
            rawUser.FullName = user.FullName;
            this.entities.Update(rawUser);
            await this.context.SaveChangesAsync();
            return user;
        }

        public async Task<BusinessObject.Models.User> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var rawUser = await this.Find(changePasswordDto.UserName);
            HashServices hashServices = new HashServices(changePasswordDto.CurrentPassword, appSetting.HashSalt);
            if (!hashServices.IsPassed(rawUser.Password)) throw new CustomerManagementException(4011);
            HashServices hashServiceNewPassword = new(changePasswordDto.NewPassword, appSetting.HashSalt);
            rawUser.Password = hashServiceNewPassword.EncryptedPassword;
            this.entities.Update(rawUser);
            await this.context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.User>(rawUser);
        }

        public async Task<BusinessObject.Models.User> Block(string username)
        {
            var rawUser = await this.Find(username);
            if (rawUser.IsAdmin)
            {
                throw new CustomerManagementException(4012);
            }
            rawUser.IsBlocked = !rawUser.IsBlocked;
            this.entities.Update(rawUser);
            await this.context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.User>(rawUser);
        }

        public async Task<BusinessObject.Models.User> Permission(BusinessObject.Models.UpdateUserPermissionDto user)
        {
            var rawUser = await this.Find(user.Username);
            rawUser.Menus.Clear();
            this.entities.Update(rawUser);

            await this.context.SaveChangesAsync();
            //if (rawUser.IsAdmin)
            //{
            //    var cannotRemoveId = new string[] { "user-setting", "menu-setting", "setting" };
            //    var menus = user.Menus.ToList();
            //    user.Menus = menus;

            //}
            foreach (var menu in user.Menus)
            {
                var rawMenu = await this.context.Menus.FindAsync(menu.MenuId);
                this.context.Menus.Attach(rawMenu);
                rawMenu.Users.Add(rawUser);

            }

            await this.context.SaveChangesAsync();
            return mapper.Map<BusinessObject.Models.User>(rawUser);
        }

        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var rawUser = await this.Find(resetPasswordDto.Username);
            if (!rawUser.Email.Equals(resetPasswordDto.Email))
            {
                throw new CustomerManagementException(4008);
            }
            string password = Extension.RandomString(10);
            HashServices hashServices = new HashServices(password, appSetting.HashSalt);
            string path = Path.Combine(webHostEnvironment.WebRootPath, "templates/email/reset-password.html");
            var resetPasswordEmailContent = File.ReadAllText(path);
            resetPasswordEmailContent = resetPasswordEmailContent.Replace("{username}", rawUser.Username);
            resetPasswordEmailContent = resetPasswordEmailContent.Replace("{fullName}", rawUser.FullName);
            resetPasswordEmailContent = resetPasswordEmailContent.Replace("{password}", password);
            var mailMessage = emailService.CreateMailMessage("Reset your acount's password", resetPasswordEmailContent, true, rawUser.Email);
            await emailService.SendMessage(mailMessage);
            rawUser.Password = hashServices.EncryptedPassword;
            this.entities.Update(rawUser);
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
