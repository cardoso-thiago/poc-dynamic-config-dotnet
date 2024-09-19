using Cardoso.Dynamic.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cardoso.Configuration.Extensions.DependencyInjection
{
    public static class LocalEnvConfigurationServiceExtensions
    {
        public static IServiceCollection AddLocalEnvConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, LocalEnvConfigurationService>();
            return services;
        }
    }
}