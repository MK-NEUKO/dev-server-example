using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Application.GetLocations;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController(
    IGetLocation getLocations,
    ILogger<LocationsController> logger
    )
    : Controller
{
    [HttpGet(Name = "GetLocations")]
    [Route("GetLocations/{query}")]
    public async Task<IEnumerable<LocationsQueryResultDto>> GetLocations(string query)
    {
        logger.LogInformation("Getting LocationsQueryResultDto for {Query}", query);
        var response = await getLocations.HandleAsync(query);
        return response;
    }
}