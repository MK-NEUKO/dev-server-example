namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public record MetadataDto
{
    public string? ModelRunUpdateTimeUtc { get; init; }
    public string? Name { get; init; }
    public int Height { get; init; }
    public string? TimezoneAbbreviation { get; init; }
    public double? Latitude { get; init; }
    public string? ModelRunUtc { get; init; }
    public double? Longitude { get; init; }
    public double UtcTimeOffset { get; init; }
    public double? GenerationTimeMs { get; init; }
}