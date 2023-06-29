using AutoMapper;
using BusinessObject;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.Base;
using Repository.Repositories.Base;
using System.Threading;

namespace Repository.Repositories
{
    internal class UserRepository : GenericRepository<BusinessObject.Models.User, Entities.User>, IUserRepository
    {
        private readonly AppSetting appSetting;
        public UserRepository(CustomerManageContext context, IMapper mapper, IOptions<AppSetting> options) : base(context, mapper)
        {
            this.appSetting = options.Value;
        }

        public async Task<(string token, DateTime expire)> Login(string username, string password)
        {

            Repository.Entities.User user = await base.Entities.FirstOrDefaultAsync(u => u.Username == username)
                ?? throw new CustomerManagementException(4040);
            var hashService = new HashServices(password, appSetting.HashSalt);
            if (hashService.IsPassed(user.Password))
            {
                return HashServices.GenerateJwtToken(appSetting.Jwt, user.Username);
            }
            throw new CustomerManagementException(4010);

        }
        public override Task<BusinessObject.Models.User> Create(BusinessObject.Models.User user)
        {
            HashServices hashServices = new(user.Password, appSetting.HashSalt);
            user.Password = hashServices.EncryptedPassword;
            return base.Create(user);
        }
    }
}
