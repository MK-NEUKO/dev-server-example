namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public record WeatherForecastDto
{
    public MetadataDto? MetadataDto { get; init; }
    public UnitsDto? UnitsDto { get; init; }
    public ForecastDataPerHourDto? ForecastDataPerHourDto { get; init; }
    public ForecastDataPerDayDto? ForecastDataPerDayDto { get; init; }
}