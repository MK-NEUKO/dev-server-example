using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Application.GetWeatherForecastHandler;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    ILogger<WeatherForecastController> logger,
    IGetWeatherForecastHandler getWeatherForecastHandler
    ) : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    [Route("GetWeatherForecast/{lat}/{lon}")]
    public async Task<WeatherForecastDto> GetWeatherForecast(double lat, double lon)
    {
        logger.LogInformation("Getting WeatherForecastDto for {Lat} and {Lon}", lat, lon);
        var response = await getWeatherForecastHandler.HandleAsync(lat, lon, CancellationToken.None);
        return response;
    }
}