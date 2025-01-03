namespace WeatherForecastApi.Application.GetLocation;

public record LocationDTO
{
    public string Name { get; init; }
    public Dictionary<string, string> LocalNames { get; init; }
    public double Lat { get; init; }
    public double Lon { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
}