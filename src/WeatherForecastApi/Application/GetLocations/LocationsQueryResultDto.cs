namespace WeatherForecastApi.Application.GetLocations;

public record LocationsQueryResultDto
{
    public string Name { get; init; }
    public double Lat { get; init; }
    public double Lon { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
}