using Microsoft.Extensions.Logging;
using System.Text.Json;
using WeatherForecastApi.DemoData;
using WeatherForecastApi.WeatherForecast;

namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public class WeatherForecastRepository(
    ILogger<WeatherForecastRepository> logger
    ) : IWeatherForecastRepository
{
    public async Task<WeatherForecast.WeatherForecast> GetWeatherForecastAsync(double lat, double lon, CancellationToken cancellationToken)
    {
        try
        {
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(WeatherForecastCopenhagen.DemoWeatherForecast));
            var weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast.WeatherForecast>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }, cancellationToken);

            if (weatherForecast == null)
            {
                throw new JsonException("Deserialization returned null.");
            }

            return weatherForecast;
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Failed to deserialize weather forecast for lat: {Lat} and lon: {Lon}", lat, lon);
            throw new BadHttpRequestException("Invalid weather forecast query result.");
        }
    }
}