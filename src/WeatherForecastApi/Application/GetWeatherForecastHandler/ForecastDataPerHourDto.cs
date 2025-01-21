namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public record ForecastDataPerHourDto
{
    public IEnumerable<string>? Time { get; init; }
    public IEnumerable<double>? SnowFraction { get; init; }
    public IEnumerable<double>? WindSpeed { get; init; }
    public IEnumerable<int>? PrecipitationProbability { get; init; }
    public IEnumerable<double>? ConvectivePrecipitation { get; init; }
    public IEnumerable<string>? RainSpot { get; init; }
    public IEnumerable<int>? PicToCode { get; init; }
    public IEnumerable<double>? FeltTemperature { get; init; }
    public IEnumerable<double>? Precipitation { get; init; }
    public IEnumerable<int>? IsDayLight { get; init; }
    public IEnumerable<int>? UvIndex { get; init; }
    public IEnumerable<int>? RelativeHumidity { get; init; }
    public IEnumerable<double>? SeaLevelPressure { get; init; }
    public IEnumerable<int>? WindDirection { get; init; }
}