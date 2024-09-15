using Cardoso.Configuration;
using Cardoso.Dynamic.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddSingleton<IConfigurationService, LocalEnvConfigurationService>();
        using IHost host = builder.Build();

        var configService = host.Services.GetRequiredService<IConfigurationService>();
        string appName = configService.GetProperty("AppSettings:ApplicationName");
        string appVersion = configService.GetProperty("AppSettings:Version");

        Console.WriteLine($"Nome da Aplicação: {appName}");
        Console.WriteLine($"Versão da Aplicação: {appVersion}");
    }
}