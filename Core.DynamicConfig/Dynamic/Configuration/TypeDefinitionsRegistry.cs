namespace Core.DynamicConfig.Dynamic.Configuration;

public static class TypeDefinitionsRegistry
{
    private static readonly Dictionary<string, Type> TypeDefinitions = new();

    public static void Register(string key, Type type)
    {
        TypeDefinitions.TryAdd(key, type);
    }

    public static Dictionary<string, Type> GetTypeDefinitions()
    {
        return TypeDefinitions;
    }
}