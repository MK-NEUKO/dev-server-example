namespace WeatherForecastApi.Infrastructure.MeteoBlueApi;

public class MeteoBlueOptions
{
    public string BaseUrlLocationQuery { get; set; }
    public string BaseUrlForecastQuery { get; set; }
    public string ApiKey { get; set; }
}