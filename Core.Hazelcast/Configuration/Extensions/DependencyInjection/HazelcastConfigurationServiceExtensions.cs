using Cardoso.Dynamic.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Hazelcast.Configuration.Extensions.DependencyInjection
{
    public static class HazelcastConfigurationServiceExtensions
    {
        public static IServiceCollection AddHazelCastConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, HazelcastConfigurationService>();
            return services;
        }
    }
}