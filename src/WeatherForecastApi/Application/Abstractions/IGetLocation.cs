using WeatherForecastApi.Application.GetLocation;

namespace WeatherForecastApi.Application.Abstractions;

public interface IGetLocation
{
    Task<IEnumerable<LocationQueryResultDto>> RequestLocations(string query);
}