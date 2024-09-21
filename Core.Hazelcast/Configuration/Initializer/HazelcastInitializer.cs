using Core.Hazelcast.Configuration.Cache;
using Hazelcast;
using Microsoft.Extensions.Hosting;

namespace Core.Hazelcast.Configuration.Initializer;

public class HazelcastInitializer(HazelcastCache cache) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var hazelcastConfiguration = GetHazelcastConfiguration().GetAwaiter().GetResult();
        cache.Configuration = hazelcastConfiguration;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
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
}