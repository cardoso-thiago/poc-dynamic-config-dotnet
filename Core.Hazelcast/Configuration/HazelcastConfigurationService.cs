using Cardoso.Dynamic.Configuration;
using Core.Hazelcast.Configuration.Cache;
using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration;

public class HazelcastConfigurationService : IConfigurationService
{
    private IConfigurationRoot _configuration;
    private HazelcastCache _cache;

    public HazelcastConfigurationService(IConfiguration configuration, HazelcastCache cache)
    {
        _cache = cache;
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddConfiguration(configuration)
            .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(_cache.Configuration);

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

    public override void UpdateAll()
    {
        //TODO o Reload aparentemente não está funcionando para o cache em memória, a investigar
        // _configuration.Reload();
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(_cache.Configuration); // Re-adiciona a coleção em memória

        _configuration = builder.Build();
    }
}