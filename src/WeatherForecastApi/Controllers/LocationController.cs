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
    public async Task<IEnumerable<LocationDTO>> Get(string City, string ZipCode = "")
    {
        return await _getLocation.RequestLocations(City, ZipCode);
    }
}