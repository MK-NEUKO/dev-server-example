using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherForecastApi.DemoData;
using WeatherForecastApi.Location;

namespace WeatherForecastApi.Services.LocationService;

internal sealed class LocationRepository(
    ILogger<LocationRepository> logger
    ) : ILocationRepository
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
        try
        {
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(LocationCopenhagen.DemoQuery));
            var locationQueryResult = await JsonSerializer.DeserializeAsync<LocationQueryResult>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }, cancellationToken);

            if (locationQueryResult == null)
            {
                throw new JsonException("Deserialization returned null.");
            }

            return locationQueryResult;
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Failed to deserialize location query result for query: {Query}", query);
            throw new BadHttpRequestException("Invalid location query result.");
        }
    }
}
