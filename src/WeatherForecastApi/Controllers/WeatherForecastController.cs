using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Application.GetWeatherForecast;
using WeatherForecastApi.Domain.Abstractions;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    IWeatherForecastService weatherForecastService,
    ILogger<WeatherForecastController> logger
    )
    : ControllerBase
{
    
}
