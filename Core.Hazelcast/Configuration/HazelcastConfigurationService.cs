using Core.DynamicConfig.Dynamic.Configuration;
using Core.Hazelcast.Configuration.Cache;
using Core.Hazelcast.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration;

public class HazelcastConfigurationService : IConfigurationService
{
    private readonly IConfigurationRoot _configuration;

    public HazelcastConfigurationService(IConfiguration configuration, HazelcastCache cache)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddConfiguration(configuration)
            .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true)
            .AddHazelcastCacheSource(cache);

        _configuration = builder.Build();
    }

    public string GetProperty(string key)
    {
        return _configuration[key] ?? "";
    }

    public string GetProperty(string key, string fallback)
    {
        return _configuration[key] ?? fallback;
    }
}