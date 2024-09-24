using Core.Hazelcast.Configuration.Cache;
using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration.Extensions;

/// <summary>
/// Extensão para utilizar o HazelcastCache como fonte de configurações no ConfigurationBuilder
/// </summary>
public static class HazelcastConfigurationExtensions
{
    public static IConfigurationBuilder AddHazelcastCacheSource(this IConfigurationBuilder builder,
        HazelcastCache hazelcastCache)
    {
        return builder.Add(new HazelcastConfigurationSource(hazelcastCache));
    }
}