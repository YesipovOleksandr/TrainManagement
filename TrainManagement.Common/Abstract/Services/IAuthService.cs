using TrainManagement.Common.Models;

namespace TrainManagement.Common.Abstract.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(User item);
        Task<User> Register(User model);
    }
}
