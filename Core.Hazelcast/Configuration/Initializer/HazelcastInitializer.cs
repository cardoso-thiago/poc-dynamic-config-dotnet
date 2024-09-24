using System.Reflection;
using System.Text.Json;
using Core.Hazelcast.Configuration.Cache;
using Hazelcast;
using Hazelcast.DistributedObjects;
using Microsoft.Extensions.Hosting;

namespace Core.Hazelcast.Configuration.Initializer;

/// <summary>
/// Inicializa o Cache local com o map custom-configuration-map-hazelcast e adiciona listeners para escutar as
/// atualizações no map
/// </summary>
public class HazelcastInitializer(HazelcastCache cache) : IHostedService
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
        string appName = Assembly.GetEntryAssembly()?.GetName().Name ?? "DefaultApplicationName";
        var options = new HazelcastOptionsBuilder().Build();
        _client = await HazelcastClientFactory.StartNewClientAsync(options);
        await using var map = await _client.GetMapAsync<string, string>(appName);

        var entries = map.GetEntriesAsync();
        foreach (var entry in entries.Result)
        {
            UpdateLocalCacheAndConfiguration(entry.Key, entry.Value);
        }

        var topic = await _client.GetTopicAsync<string>("configuration." + appName);
        await topic.SubscribeAsync(messageEvent => { messageEvent.Message(HandleTopicMessage); });
    }

    private void HandleTopicMessage(IHTopic<string> hTopic, TopicMessageEventArgs<string> topicMessageEventArgs)
    {
        try
        {
            var message = topicMessageEventArgs.Payload;
            var deserializedMessage = JsonSerializer.Deserialize<Dictionary<string, string>>(message);

            if (deserializedMessage == null || !deserializedMessage.TryGetValue("value", out var value)) return;
            var key = deserializedMessage["key"];
            UpdateLocalCacheAndConfiguration(key, value);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Erro ao desserializar a mensagem: {ex.Message}");
        }
    }

    private void UpdateLocalCacheAndConfiguration(string key, string value)
    {
        cache.CacheDict[key] = value;
        cache.Update();
    }
}