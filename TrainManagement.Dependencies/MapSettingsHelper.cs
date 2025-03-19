using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainManagement.Common.Models.Settings;

namespace TrainManagement.Dependencies
{
    public static class MapSettingsHelper
    {
        public static void MapSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(settings =>
            {
                settings.JWTOptions = configuration.GetSection("JWT").Get<JWTOptions>();
            });
        }
    }
}