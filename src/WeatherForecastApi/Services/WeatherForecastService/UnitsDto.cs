namespace WeatherForecastApi.Services.WeatherForecastService;

public record UnitsDto
{
    public string Predictebility { get; init; }
    public string Precipitation { get; init; }
    public string Windspeed { get; init; }
    public string PrecipitationProbability { get; init; }
    public string Relativehumidity { get; init; }
    public string Temperature { get; init; }
    public string Time { get; init; }
    public string Pressure { get; init; }
    public string Winddirection { get; init; }
}