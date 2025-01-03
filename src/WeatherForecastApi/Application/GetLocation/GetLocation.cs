using WeatherForecastApi.Application.GetLocation.Abstractions;
using WeatherForecastApi.Domain.Abstractions;

namespace WeatherForecastApi.Application.GetLocation;

public class GetLocation : IGetLocation
{
    private readonly ILocationService _locationService;

    public GetLocation(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<IEnumerable<LocationDTO>> RequestLocations(string City, string ZipCode = "")
    {
        var response = await _locationService.GetLocationAsync(City, ZipCode);

        var locations = response.Select(location => new LocationDTO
        {
            Name = location.Name,
            LocalNames = location.LocalNames,
            Lat = location.Lat,
            Lon = location.Lon,
            Country = location.Country,
            State = location.State
        }).ToList();

        return await Task.FromResult(locations);
    }
}