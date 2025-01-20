using WeatherForecastApi.Services.LocationService;

namespace WeatherForecastApi.Services.Abstractions;

public interface ILocationService
{
    Task<LocationQueryResultDto> HandleAsync(string query, CancellationToken cancellationToken);
}