using Core.DynamicConfig.Dynamic.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Core.LocalEnvConfig.Configuration;

public class CustomObjectsService : ICustomObjectsService
{
    private readonly IConfigurationRoot _configuration;
    private readonly Dictionary<string, object> _cache;

    public CustomObjectsService(IConfiguration configuration, Dictionary<string, Type>? typeDefinitions)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddConfiguration(configuration)
            .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true);

        _configuration = builder.Build();

        typeDefinitions ??= new Dictionary<string, Type>();
        var frameworkDefinitions = TypeDefinitionsRegistry.GetTypeDefinitions();
        foreach (var typeDefinition in frameworkDefinitions)
        {
            typeDefinitions.TryAdd(typeDefinition.Key, typeDefinition.Value);
        }
        _cache = CreateCache(typeDefinitions);
    }

    private Dictionary<string, object> CreateCache(Dictionary<string, Type> typeDefinitions)
    {
        Dictionary<string, object> cache = new();
        foreach (var entry in typeDefinitions)
        {
            var jsonValue = _configuration[entry.Key];
            if (jsonValue != null)
            {
                var deserializedValue = JsonConvert.DeserializeObject(jsonValue, entry.Value);
                if (deserializedValue is not null)
                {
                    cache.Add(entry.Key, deserializedValue);
                }
            }
        }

        return cache;
    }

    public T Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var value))
        {
            return (T)value;
        }

        throw new KeyNotFoundException($"Chave '{key}' não encontrada no cache.");
    }
}