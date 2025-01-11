using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecastApi.Domain.Abstractions;
using WeatherForecastApi.Domain.Location;

namespace WeatherForecastApi.Infrastructure.MeteoBlueApi;

public class MeteoBlueLocationsService(
    IOptions<MeteoBlueOptions> options,
    HttpClient httpClient,
    ILogger<MeteoBlueLocationsService> logger
    )
    : ILocationsService
{
    private readonly MeteoBlueOptions _options = options.Value;

    public async Task<LocationQueryResult> GetLocationsAsync(string query)
    {
        logger.LogInformation("Querying location for {Query}", query);

        var url = $"{_options.BaseUrlLocationQuery}?query={query}&apikey={_options.ApiKey}";
        var response = await httpClient.GetFromJsonAsync<LocationQueryResult>(url);

        return response;
    }
}