using TrainManagement.Common.Models;

namespace TrainManagement.Common.Abstract.Services
{
    public interface IUserService
    {
        Task<User> Create(User model);
        Task<User> GetByLogin(string login);
        Task Update(User user);
    }
}
