using Core.DynamicConfig.Dynamic.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.AnotherLib.Extensions.DependencyInjection;

public static class AnotherLibExtensions
{
    public static IServiceCollection AddAnotherLibConfiguration(this IServiceCollection services)
    {
        TypeDefinitionsRegistry.Register("AnotherLib.CustomType", typeof(List<string>));
        return services;
    }
}