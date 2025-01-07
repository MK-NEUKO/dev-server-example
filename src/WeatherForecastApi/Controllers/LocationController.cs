using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Application.GetLocation;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : Controller
{
    private readonly IGetLocation _getLocation;
    private readonly ILogger<LocationController> _logger;

    public LocationController(IGetLocation getLocation, ILogger<LocationController> logger)
    {
        _getLocation = getLocation;
        _logger = logger;
    }

    [HttpGet(Name = "GetLocations")]
    [Route("GetLocations/{query}")]
    public async Task<IEnumerable<LocationQueryResultDto>> GetLocations(string query)
    {
        _logger.LogInformation("Getting LocationQueryResultDto for {Query}", query);
        var response = await _getLocation.RequestLocations(query);
        return response;
    }
}