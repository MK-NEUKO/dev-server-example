using WeatherForecastApi.Application.GetWeatherForecastHandler;

namespace WeatherForecastApi.Application.Abstractions;

public interface IGetWeatherForecastHandler
{
    Task<WeatherForecastDto> HandleAsync(double lat, double lon, CancellationToken cancellationToken);
}