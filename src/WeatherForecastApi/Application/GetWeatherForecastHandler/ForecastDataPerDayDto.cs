namespace WeatherForecastApi.Application.GetWeatherForecastHandler;

public record ForecastDataPerDayDto
{
    // deo respektive zu ForecastDataPerDay
    public IEnumerable<string>? Time { get; init; }
    public IEnumerable<double>? TemperatureInstant { get; init; }
    public IEnumerable<double>? Precipitation { get; init; }
    public IEnumerable<int>? Predictability { get; init; }
    public IEnumerable<double>? TemperatureMax { get; init; }
    public IEnumerable<int>? SeaLevelPressureMean { get; init; }
    public IEnumerable<double>? WindSpeedMean { get; init; }
    public IEnumerable<double>? PrecipitationHours { get; init; }
    public IEnumerable<int>? SeaLevelPressureMin { get; init; }
    public IEnumerable<int>? PictogramCode { get; init; }
    public IEnumerable<double>? SnowFraction { get; init; }
    public IEnumerable<double>? HumidityGreater90Hours { get; init; }
    public IEnumerable<double>? ConvectivePrecipitation { get; init; }
    public IEnumerable<int>? RelativeHumidityMax { get; init; }
    public IEnumerable<double>? TemperatureMin { get; init; }
    public IEnumerable<int>? WindDirection { get; init; }
    public IEnumerable<double>? FeltTemperatureMax { get; init; }
    public IEnumerable<int>? IndexTo1HValuesEnd { get; init; }
    public IEnumerable<int>? RelativeHumidityMin { get; init; }
    public IEnumerable<double>? FeltTemperatureMean { get; init; }
    public IEnumerable<double>? WindSpeedMin { get; init; }
    public IEnumerable<double>? FeltTemperatureMin { get; init; }
    public IEnumerable<int>? PrecipitationProbability { get; init; }
    public IEnumerable<int>? UvIndex { get; init; }
    public IEnumerable<int>? IndexTo1HValuesStart { get; init; }
    public IEnumerable<string>? RainSpot { get; init; }
    public IEnumerable<double>? TemperatureMean { get; init; }
    public IEnumerable<int>? SeaLevelPressureMax { get; init; }
    public IEnumerable<int>? RelativeHumidityMean { get; init; }
    public IEnumerable<double>? PredictabilityClass { get; init; }
    public IEnumerable<double>? WindSpeedMax { get; init; }
}