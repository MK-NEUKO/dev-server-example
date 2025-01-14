using WeatherForecastApi.Domain.Location;

namespace WeatherForecastApi.Domain.Abstractions;

public interface ILocationsService
{
    Task<LocationQueryResult> GetLocationsAsync(string query);
}