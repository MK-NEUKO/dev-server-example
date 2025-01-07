using AutoMapper;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Domain.Abstractions;

namespace WeatherForecastApi.Application.GetLocation;

public class GetLocation(
    ILocationService locationService,
    IMapper mapper
    ) 
    : IGetLocation
{
    public async Task<IEnumerable<LocationQueryResultDto>> RequestLocations(string query)
    {
        var response = await locationService.GetLocationAsync(query);

        var locations = new List<LocationQueryResultDto>();
        foreach (var responseResult in response.Results)
        {
            var location = mapper.Map<LocationQueryResultDto>(responseResult);
            locations.Add(location);
        }

        return locations;
    }
}