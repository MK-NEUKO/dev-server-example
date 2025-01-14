using WeatherForecastApi.Application.Abstractions;

namespace WeatherForecastApi.Application.GetWeatherForecast;

public class GetWeatherForecast : IGetWeatherForecast
{
    public Task<WeatherForecastDto> HandleAsync(string query)
    {
        throw new NotImplementedException();
    }
}