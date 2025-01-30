using System.Text.Json.Serialization;

namespace WeatherForecastApi.WeatherForecast;

public record WeatherForecast(
    [property: JsonPropertyName("metadata")] Metadata Metadata,
    [property: JsonPropertyName("units")] Units Units,
    [property: JsonPropertyName("data_1h")] ForecastDataPerHour ForecastDataPerHour,
    [property: JsonPropertyName("data_day")] ForecastDataPerDay ForecastDataPerDay
    );