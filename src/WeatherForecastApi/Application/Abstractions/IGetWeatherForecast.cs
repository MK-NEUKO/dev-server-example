using WeatherForecastApi.Application.GetWeatherForecast;

namespace WeatherForecastApi.Application.Abstractions;

public interface IGetWeatherForecast
{
    Task<WeatherForecastDto> HandleAsync(string query);
}