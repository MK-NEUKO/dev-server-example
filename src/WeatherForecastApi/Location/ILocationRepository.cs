namespace WeatherForecastApi.Location;

internal interface ILocationRepository
{
    Task<LocationQueryResult?> GetLocationsAsync(string query, CancellationToken cancellationToken);
}