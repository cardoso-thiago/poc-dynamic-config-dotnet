using Cardoso.Dynamic.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cardoso.Configuration.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddLocalEnvConfiguration();
        using IHost host = builder.Build();

        var configService = host.Services.GetRequiredService<IConfigurationService>();
        string appName = configService.GetProperty("AppSettings:ApplicationName");
        string appVersion = configService.GetProperty("AppSettings:Version");
        string additionalAppName = configService.GetProperty("AdditionalAppSettings:ApplicationName");
        string additionalAppVersion = configService.GetProperty("AdditionalAppSettings:Version");

        Console.WriteLine($"Nome da Aplicação: {appName}");
        Console.WriteLine($"Versão da Aplicação: {appVersion}");
        Console.WriteLine($"Nome da Aplicação Adicional: {additionalAppName}");
        Console.WriteLine($"Versão da Aplicação Adicional: {additionalAppVersion}");
    }
}