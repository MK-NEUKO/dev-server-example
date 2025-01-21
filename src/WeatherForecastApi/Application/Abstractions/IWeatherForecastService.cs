using WeatherForecastApi.Application.GetWeatherForecastHandler;

namespace WeatherForecastApi.Application.Abstractions;

public interface IWeatherForecastService
{
    Task<WeatherForecastDto> HandleAsync(double lat, double lon, CancellationToken cancellationToken);
}