using System.Text.Json;
using WeatherForecastApi.DemoData;
using WeatherForecastApi.WeatherForecast;

namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    public async Task<WeatherForecast.WeatherForecast> GetWeatherForecastAsync(double lat, double lon, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(WeatherForecastCopenhagen.DemoWeatherForecast));
        var weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast.WeatherForecast>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }, cancellationToken);

        return weatherForecast;
    }
}