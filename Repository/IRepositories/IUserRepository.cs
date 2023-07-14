using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IUserRepository : IGenericRepository<BusinessObject.Models.User, Entities.User>
    {
        Task<BusinessObject.Models.LoginResponse> Login(string username, string password);
        new Task<BusinessObject.Models.User> Create(BusinessObject.Models.User user);
        Task<BusinessObject.Models.User> UpdateInformation(BusinessObject.Models.UpdateUserInformationDto user);
    }
}
