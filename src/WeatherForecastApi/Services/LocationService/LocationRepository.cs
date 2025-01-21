using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherForecastApi.DemoData;
using WeatherForecastApi.Location;

namespace WeatherForecastApi.Services.LocationService;

public class LocationRepository : ILocationRepository
{
    /// <summary>
    /// Retrieves location data based on the provided query.
    /// This is a demo API call that reads demo data from LocationCopenhagen.
    /// </summary>
    /// <param name="query">The search query for locations.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the location query result.</returns>
    public async Task<LocationQueryResult?> GetLocationsAsync(string query, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(LocationCopenhagen.DemoQuery));
        var locationQueryResult = await JsonSerializer.DeserializeAsync<LocationQueryResult>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }, cancellationToken);

        return locationQueryResult ?? new LocationQueryResult
        (
            query: query,
            iso2: "",
            currentPage: 0,
            itemsPerPage: 0,
            pages: 0,
            count: 0,
            orderBy: "",
            lat: 0.0,
            lon: 0.0,
            radius: 0,
            type: "",
            results: []
        );
    }
}
