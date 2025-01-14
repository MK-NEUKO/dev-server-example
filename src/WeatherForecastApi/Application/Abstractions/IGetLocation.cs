using WeatherForecastApi.Application.GetLocations;

namespace WeatherForecastApi.Application.Abstractions;

public interface IGetLocation
{
    Task<IEnumerable<LocationsQueryResultDto>> HandleAsync(string query);
}