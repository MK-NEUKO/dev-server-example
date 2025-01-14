using AutoMapper;
using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Domain.Abstractions;

namespace WeatherForecastApi.Application.GetLocations;

public class GetLocations(
    ILocationsService locationsesService,
    IMapper mapper
    ) 
    : IGetLocation
{
    public async Task<IEnumerable<LocationsQueryResultDto>> HandleAsync(string query)
    {
        var response = await locationsesService.GetLocationsAsync(query);

        var locations = new List<LocationsQueryResultDto>();
        foreach (var responseResult in response.Results)
        {
            var location = mapper.Map<LocationsQueryResultDto>(responseResult);
            locations.Add(location);
        }

        return locations;
    }
}