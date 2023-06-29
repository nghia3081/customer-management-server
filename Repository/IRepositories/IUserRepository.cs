using Repository.IRepositories.Base;

namespace Repository.IRepositories
{
    public interface IUserRepository : IGenericRepository<BusinessObject.Models.User, Entities.User>
    {
        Task<(string token, DateTime expire)> Login(string username, string password);
        new Task<BusinessObject.Models.User> Create(BusinessObject.Models.User user);
    }
}
