using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Services.Abstractions;
using WeatherForecastApi.Services.WeatherForecastService;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    ILogger<WeatherForecastController> logger,
    IWeatherForecastService weatherForecastService
    ) : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    [Route("GetWeatherForecast/{lat}/{lon}")]
    public async Task<WeatherForecastDto> GetWeatherForecast(double lat, double lon)
    {
        logger.LogInformation("Getting WeatherForecastDto for {Lat} and {Lon}", lat, lon);
        var response = await weatherForecastService.HandleAsync(lat, lon, CancellationToken.None);
        return response;
    }
}