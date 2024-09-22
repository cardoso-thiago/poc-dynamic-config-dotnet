using Cardoso.Dynamic.Configuration;
using Core.Hazelcast.Configuration.Cache;
using Hazelcast;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Hazelcast.Configuration.Initializer;

public class HazelcastInitializer(HazelcastCache cache, IServiceProvider serviceProvider) : IHostedService
{
    private IHazelcastClient? _client;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        InitializeCacheFromHazelcast().GetAwaiter().GetResult();
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_client != null) await _client.DisposeAsync();
    }

    private async Task InitializeCacheFromHazelcast()
    {
        var options = new HazelcastOptionsBuilder().Build();
        _client = await HazelcastClientFactory.StartNewClientAsync(options);
        await using var map = await _client.GetMapAsync<string, string>("custom-configuration-map-hazelcast");

        await map.SubscribeAsync(events => events
            .EntryAdded((_, args) => { UpdateLocalCacheAndConfiguration(args.Key, args.Value); })
            .EntryRemoved((_, args) => { UpdateLocalCacheAndConfiguration(args.Key, remove: true); })
            .EntryUpdated((_, args) => { UpdateLocalCacheAndConfiguration(args.Key, args.Value); })
        );

        var entries = map.GetEntriesAsync();
        foreach (var entry in entries.Result)
        {
            cache.Configuration[entry.Key] = entry.Value;
        }
    }

    private void UpdateLocalCacheAndConfiguration(string key, string value = "", bool remove = false)
    {
        if (remove)
        {
            cache.Configuration.Remove(key);
        }
        else
        {
            cache.Configuration[key] = value;
        }

        using var scope = serviceProvider.CreateScope();
        var configurationService = scope.ServiceProvider.GetRequiredService<IConfigurationService>();
        configurationService.UpdateAll();
    }
}