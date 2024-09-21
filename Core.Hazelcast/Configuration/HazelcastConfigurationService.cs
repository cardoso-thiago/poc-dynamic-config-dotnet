using Cardoso.Dynamic.Configuration;
using Hazelcast;
using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration;

public class HazelcastConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public HazelcastConfigurationService(IConfiguration configuration)
    {
        var hazelcastConfiguration = GetHazelcastConfiguration().GetAwaiter().GetResult();
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddConfiguration(configuration)
            .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(hazelcastConfiguration);

        _configuration = builder.Build();
    }

    private async Task<Dictionary<string, string?>> GetHazelcastConfiguration()
    {
        var options = new HazelcastOptionsBuilder().Build();

        await using var client = await HazelcastClientFactory.StartNewClientAsync(options);
        await using var map = await client.GetMapAsync<string, string>("custom-configuration-map-hazelcast");

        var externalConfigurations = new Dictionary<string, string?>();
        var entries = map.GetEntriesAsync();
        foreach (var entry in entries.Result)
        {
            externalConfigurations[entry.Key] = entry.Value;
        }

        return externalConfigurations;
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