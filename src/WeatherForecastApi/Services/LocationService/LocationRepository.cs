using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherForecastApi.DemoData;
using WeatherForecastApi.Location;

namespace WeatherForecastApi.Services.LocationService;

public class LocationRepository : ILocationRepository
{
    public async Task<LocationQueryResult> GetLocationsAsync(string query, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(LocationCopenhagen.DemoQuery));
        var locationQueryResult = await JsonSerializer.DeserializeAsync<LocationQueryResult>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }, cancellationToken);

        return locationQueryResult;
    }
}
