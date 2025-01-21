using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Services.Abstractions;
using WeatherForecastApi.Services.LocationService;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController(
    ILocationService locationService,
    ILogger<LocationsController> logger
    )
    : ControllerBase
{
    [HttpGet(Name = "GetLocations")]
    [Route("GetLocations/{query}")]
    public async Task<LocationQueryResultDto> GetLocations(string query)
    {
        logger.LogInformation("Getting LocationsQueryResultDto for {Query}", query);
        var response = await locationService.HandleAsync(query, CancellationToken.None);
        return response;
    }
}