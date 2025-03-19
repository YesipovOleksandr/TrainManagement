using TrainManagement.Common.Models;

namespace TrainManagement.Common.Abstract.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByLogin(string login);
        Task<User> Create(User model);
        Task Update(User model);
    }
}
