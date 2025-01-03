namespace WeatherForecastApi.Infrastructure;

public class OpenWeatherMapOptions
{
    public string ApiKey { get; init; }
    public string BaseUrlLocation { get; init; }
    public string BaseUrlForecast { get; init; }
}