using Cardoso.Dynamic.Configuration;
using Core.Hazelcast.Configuration.Cache;
using Core.Hazelcast.Configuration.Initializer;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Hazelcast.Configuration.Extensions.DependencyInjection
{
    public static class HazelcastConfigurationServiceExtensions
    {
        public static IServiceCollection AddHazelCastConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<HazelcastCache>();
            services.AddHostedService<HazelcastInitializer>();
            services.AddSingleton<IConfigurationService, HazelcastConfigurationService>();
            return services;
        }
    }
}