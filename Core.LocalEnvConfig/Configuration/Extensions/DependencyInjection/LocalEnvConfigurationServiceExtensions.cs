using Core.DynamicConfig.Dynamic.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.LocalEnvConfig.Configuration.Extensions.DependencyInjection
{
    public static class LocalEnvConfigurationServiceExtensions
    {
        public static IServiceCollection AddLocalEnvConfiguration(this IServiceCollection services,
            Dictionary<string, Type>? typeDefinitions = null)
        {
            services.AddSingleton<ICustomObjectsService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new CustomObjectsService(configuration, typeDefinitions);
            });
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            return services;
        }
    }
}