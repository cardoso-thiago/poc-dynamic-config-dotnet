using Cardoso.Dynamic.Configuration;
using Core.Hazelcast.Configuration.Cache;
using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration;

public class HazelcastConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public HazelcastConfigurationService(IConfiguration configuration, HazelcastCache cache)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddConfiguration(configuration)
            .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(cache.Configuration);

        _configuration = builder.Build();
    }

    public override string GetProperty(string key)
    {
        return _configuration[key] ?? "";
    }

    public override string GetProperty(string key, string fallback)
    {
        return _configuration[key] ?? fallback;
    }
}