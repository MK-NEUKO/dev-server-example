namespace WeatherForecastApi.WeatherForecast;

public interface IWeatherForecastRepository
{
    Task<WeatherForecast> GetWeatherForecastAsync(double lat, double lon, CancellationToken cancellationToken);
}