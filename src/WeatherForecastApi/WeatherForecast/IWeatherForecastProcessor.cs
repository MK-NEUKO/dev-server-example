namespace WeatherForecastApi.WeatherForecast;

public interface IWeatherForecastProcessor
{
    List<ForecastDataPerHour> ProcessPerDayPerHour(ForecastDataPerHour data);
}