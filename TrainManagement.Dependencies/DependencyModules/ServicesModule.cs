using TrainManagement.BLL.Services;
using TrainManagement.Common.Abstract.Repository;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace TrainManagement.Dependencies.DependencyModules
{
    public static class ServicesModule
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IHasher, Hasher>();
        }
    }
}
