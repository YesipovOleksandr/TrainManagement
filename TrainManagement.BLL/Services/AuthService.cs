using TrainManagement.Common.Abstract.Repository;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.Common.Enums;
using TrainManagement.Common.Models;
using TrainManagement.Common.Models.Settings;
using Microsoft.Extensions.Options;

namespace TrainManagement.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IHasher _hasher;
        private readonly JWTOptions _jWTOptions;

        public AuthService(IUserService userService,
                           IHasher hasher,
                           ITokenService tokenService,
                           IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _tokenService = tokenService;
            _hasher = hasher;
            _jWTOptions = appSettings.Value.JWTOptions;
        }

        public async Task<User> Authenticate(User model)
        {
            var user = await _userService.GetByLogin(model.Login);

            if (user == null)
            {
                return null;
            }

            if (!_hasher.Сompare(user.Password, model.Password))
            {
                return null;
            }


            user.Token = _tokenService.GenerateAccessToken(user);
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_jWTOptions.TokenLongLifeTime);
            user.RefreshToken = _tokenService.GenerateRefreshToken();

            await _userService.Update(user);
            return user;
        }
        public async Task<User> Register(User model)
        {
            var existingUser = await _userService.GetByLogin(model.Login);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            model.Password = _hasher.GetHash(model.Password);
            model.RefreshToken = _tokenService.GenerateRefreshToken();
            model.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_jWTOptions.TokenLongLifeTime);

            var newUser = await _userService.Create(model);

            return newUser;
        }
    }
}