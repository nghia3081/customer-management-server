using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IUserRepository : IGenericRepository<BusinessObject.Models.User, Entities.User>
    {
        Task<BusinessObject.Models.LoginResponse> Login(string username, string password);
        new Task<BusinessObject.Models.User> Create(BusinessObject.Models.User user);
        Task<BusinessObject.Models.User> UpdateInformation(BusinessObject.Models.UpdateUserInformationDto user);
        Task<BusinessObject.Models.User> ChangePassword(BusinessObject.Models.ChangePasswordDto changePasswordDto);
        Task<BusinessObject.Models.User> Block(string username);
        Task<BusinessObject.Models.User> Permission(BusinessObject.Models.UpdateUserPermissionDto user);
        Task<bool> ResetPassword(BusinessObject.Models.ResetPasswordDto resetPasswordDto);
    }
}
