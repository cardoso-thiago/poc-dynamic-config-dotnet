using Cardoso.Dynamic.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    ILogger<WeatherForecastController> logger,
    IConfigurationService configurationService)
    : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        logger.LogInformation("Nome da Aplicação: {AppName}",
            configurationService.GetProperty("AppSettings:ApplicationName"));
        logger.LogInformation("Nome da Aplicação Adicional: {AdditionalAppName}",
            configurationService.GetProperty("AdditionalAppSettings:ApplicationName"));
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}