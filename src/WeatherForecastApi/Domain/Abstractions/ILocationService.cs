using WeatherForecastApi.Domain.Location;

namespace WeatherForecastApi.Domain.Abstractions;

public interface ILocationService
{
    Task<LocationQueryResult> GetLocationAsync(string query);
}