namespace Core.DynamicConfig.Dynamic.Configuration;

public interface ICustomObjectsService
{
    T Get<T>(string key);
}