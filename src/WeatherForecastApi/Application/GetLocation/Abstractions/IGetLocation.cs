namespace WeatherForecastApi.Application.GetLocation.Abstractions;

public interface IGetLocation
{
    Task<IEnumerable<LocationDTO>> RequestLocations(string City, string ZipCode = "");
}