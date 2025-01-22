namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public record UnitsDto
{
    public string? Predictability { get; init; }
    public string? Precipitation { get; init; }
    public string? WindSpeed { get; init; }
    public string? PrecipitationProbability { get; init; }
    public string? RelativeHumidity { get; init; }
    public string? Temperature { get; init; }
    public string? Time { get; init; }
    public string? Pressure { get; init; }
    public string? WindDirection { get; init; }
}