using WeatherForecastApi.Application.GetLocationHandler;

namespace WeatherForecastApi.Application.Abstractions;

public interface IGetLocationHandler
{
    Task<LocationQueryResultDto> HandleAsync(string query, CancellationToken cancellationToken);
}