namespace WeatherForecastApi.Location;

public interface ILocationRepository
{
    Task<LocationQueryResult?> GetLocationsAsync(string query, CancellationToken cancellationToken);
}