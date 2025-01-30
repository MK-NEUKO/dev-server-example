namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public record WeatherForecastDto
{
    public MetadataDto? Metadata { get; init; }
    public UnitsDto? Units { get; init; }
    public IEnumerable<ForecastDataPerHourDto>? ForecastDataPerDayPerHour { get; init; }
    public ForecastDataPerDayDto? ForecastDataPerDay { get; init; }
}