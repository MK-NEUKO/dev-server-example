namespace WeatherForecastApi.Services.WeatherForecastService;

public record MetadataDto
{
    public string ModelRunUpdateTimeUtc { get; init; }
    public string? Name { get; init; }
    public int Height { get; init; }
    public string TimezoneAbbrevation { get; init; }
    public double Latidude { get; init; }
    public string ModelRunUtc { get; init; }
    public double Longitude { get; init; }
    public double UtcTimeoffset { get; init; }
    public double GenerationTimeMs { get; init; }
}