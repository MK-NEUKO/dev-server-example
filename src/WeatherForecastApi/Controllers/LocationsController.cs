using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Application.GetLocationHandler;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController(
    IGetLocationHandler getLocationHandler,
    ILogger<LocationsController> logger
    )
    : ControllerBase
{
    [HttpGet(Name = "GetLocations")]
    [Route("GetLocations/{query}")]
    public async Task<LocationQueryResultDto> GetLocations(string query)
    {
        logger.LogInformation("Getting LocationsQueryResultDto for {Query}", query);
        var response = await getLocationHandler.HandleAsync(query, CancellationToken.None);
        return response;
    }
}