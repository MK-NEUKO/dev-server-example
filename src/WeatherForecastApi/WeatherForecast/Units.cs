using System.Text.Json.Serialization;

namespace WeatherForecastApi.WeatherForecast;

public record Units(
    [property: JsonPropertyName("predictability")] string Predictebility,
    [property: JsonPropertyName("precipitation")] string Precipitation,
    [property: JsonPropertyName("windspeed")] string Windspeed,
    [property: JsonPropertyName("precipitation_probability")] string PrecipitationProbability,
    [property: JsonPropertyName("relativehumidity")] string Relativehumidity,
    [property: JsonPropertyName("temperature")] string Temperature,
    [property: JsonPropertyName("time")] string Time,
    [property: JsonPropertyName("pressure")] string Pressure,
    [property: JsonPropertyName("winddirection")] string Winddirection
);