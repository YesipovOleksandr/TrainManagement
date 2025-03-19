using TrainManagement.Dependencies.DependencyModules;
using Microsoft.Extensions.DependencyInjection;

namespace TrainManagement.Dependencies
{
    public static class DependencyRegistrator
    {
        public static void RegisterDependencyModules(this IServiceCollection services)
        {
            services.RegisterServices();
        }
    }
}
