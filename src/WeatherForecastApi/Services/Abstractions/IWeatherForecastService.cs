using WeatherForecastApi.Services.WeatherForecastService;

namespace WeatherForecastApi.Services.Abstractions;

public interface IWeatherForecastService
{
    Task<WeatherForecastDto> HandleAsync(double lat, double lon, CancellationToken cancellationToken);
}