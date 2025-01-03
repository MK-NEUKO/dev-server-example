using System.Net.Http;
using WeatherForecastApi.Domain.Abstractions;
using WeatherForecastApi.Domain.Location;

namespace WeatherForecastApi.Infrastructure;

public class OpenWeatherMapLocationService : ILocationService
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherMapOptions _options;

    public OpenWeatherMapLocationService(HttpClient httpClient, OpenWeatherMapOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<List<Location>> GetLocationAsync(string city, string zipCode)
    {
        var locationRequestUri = _options.BaseUrlLocation
            .Replace("{city name}", city)
            .Replace("{state code},{country code}", zipCode)
            .Replace("{limit}", "5")
            .Replace("{API key}", _options.ApiKey);
        var locationResponse = await _httpClient.GetFromJsonAsync<List<Location>>(locationRequestUri);

        return await Task.FromResult(locationResponse);
    }
}