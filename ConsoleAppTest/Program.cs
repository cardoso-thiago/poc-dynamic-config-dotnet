using ConsoleAppTest;
using Core.AnotherLib.Extensions.DependencyInjection;
using Core.DynamicConfig.Dynamic.Configuration;
using Core.LocalEnvConfig.Configuration.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAnotherLibConfiguration();
var typeDefinitions = new Dictionary<string, Type>
{
    { "Cliente.Config", typeof(Cliente) }
};
builder.Services.AddLocalEnvConfiguration(typeDefinitions);
builder.Configuration.AddJsonFile("customappsettings.json", true, true);
using IHost host = builder.Build();

var configService = host.Services.GetRequiredService<IConfigurationService>();
var customObjectsService = host.Services.GetRequiredService<ICustomObjectsService>();
var appName = configService.GetProperty("AppSettings:ApplicationName");
var appVersion = configService.GetProperty("AppSettings:Version");
var additionalAppName = configService.GetProperty("AdditionalAppSettings:ApplicationName");
var additionalAppVersion = configService.GetProperty("AdditionalAppSettings:Version");
var customConfig = configService.GetProperty("CustomSettings:MyCustomKey");

Console.WriteLine($"Nome da Aplicação: {appName}");
Console.WriteLine($"Versão da Aplicação: {appVersion}");
Console.WriteLine($"Nome da Aplicação Adicional: {additionalAppName}");
Console.WriteLine($"Versão da Aplicação Adicional: {additionalAppVersion}");
Console.WriteLine($"Configuração Customizada: {customConfig}");
var cliente = customObjectsService.Get<Cliente>("Cliente.Config");
var listFromAnotherLib = customObjectsService.Get<List<String>>("AnotherLib.CustomType");
Console.WriteLine($"Nome do cliente: {cliente.Nome}");
Console.WriteLine($"CPF do cliente: {cliente.Cpf}");
foreach (var listValue in listFromAnotherLib)
{
    Console.WriteLine($"Item da lista configurada: {listValue}");    
}