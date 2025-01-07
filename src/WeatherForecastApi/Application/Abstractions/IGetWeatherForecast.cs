using WeatherForecastApi.Application.GetLocation;
using WeatherForecastApi.Application.GetWeatherForecast;

namespace WeatherForecastApi.Application.Abstractions;

public interface IGetWeatherForecast
{
    Task<WeatherForecastDTO> RequestWeatherForecast(LocationQueryResultDto locationQueryResult);
}