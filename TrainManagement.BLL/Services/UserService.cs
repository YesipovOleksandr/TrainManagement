using TrainManagement.Common.Abstract.Repository;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.Common.Models;

namespace TrainManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User model)
        {
            return await _userRepository.Create(model);
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _userRepository.GetByLogin(login);
        }

        public async Task Update(User user)
        {
            await _userRepository.Update(user);
        }
    }
}
