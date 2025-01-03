using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Application.GetLocation;
using WeatherForecastApi.Application.GetLocation.Abstractions;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : Controller
{
    private readonly IGetLocation _getLocation;

    public LocationController(IGetLocation getLocation)
    {
        _getLocation = getLocation;
    }

    [HttpGet(Name = "GetLocations")]
    [Route("GetLocations/{city}")]
    public async Task<IEnumerable<LocationDTO>> GetLocations(string city, string ZipCode = "")
    {
        var response = await _getLocation.RequestLocations(city, ZipCode);
        return response.ToArray();
    }
}