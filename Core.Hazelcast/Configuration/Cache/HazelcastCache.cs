using Microsoft.Extensions.Configuration;

namespace Core.Hazelcast.Configuration.Cache;

/// <summary>
/// Armazena o Dictionary com as configurações do map do Hazelcast. É um ConfigurationProvider, para poder ser
/// utilizado como fonte padrão de configurações
public class HazelcastCache : ConfigurationProvider
{
    public Dictionary<string, string?> CacheDict { get; } = new();

    public override void Load()
    {
        Data = new Dictionary<string, string?>(CacheDict);
    }

    public void Update()
    {
        //Recarrega os dados para que o IConfigurationRoot seja atualizado
        Load();
        OnReload();
    }
}