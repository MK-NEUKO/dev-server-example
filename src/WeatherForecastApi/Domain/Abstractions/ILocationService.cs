namespace WeatherForecastApi.Domain.Abstractions;

public interface ILocationService
{
    Task<List<Location.Location>> GetLocationAsync(string city, string zipCode);
}