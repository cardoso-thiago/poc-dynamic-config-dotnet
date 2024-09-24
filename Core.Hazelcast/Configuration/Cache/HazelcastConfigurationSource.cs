using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration.Cache;

/// <summary>
/// Implementação do cache como ConfigurationSource, para poder ser utilizado como extensão para o ConfigurationBuilder
/// </summary>
public class HazelcastConfigurationSource(HazelcastCache hazelcastCache) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return hazelcastCache;
    }
}