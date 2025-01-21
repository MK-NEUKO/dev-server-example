namespace WeatherForecastApi.Services.LocationService;

public sealed record LocationQueryResultDto
{
    public string Query { get; init; }
    public string Iso2 { get; init; }
    public int CurrentPage { get; init; }
    public int ItemsPerPage { get; init; }
    public int Pages { get; init; }
    public int Count { get; init; }
    public string OrderBy { get; init; }
    public double? Lat { get; init; }
    public double? Lon { get; init; }
    public int Radius { get; init; }
    public string Type { get; init; }
    public IEnumerable<LocationDto> Results { get; init; }
}