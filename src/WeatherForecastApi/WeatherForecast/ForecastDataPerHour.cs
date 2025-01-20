using System.Text.Json.Serialization;

namespace WeatherForecastApi.WeatherForecast;

public record ForecastDataPerHour(
    [property: JsonPropertyName("time")] IEnumerable<DateTime> Time,
    [property: JsonPropertyName("snowfraction")] IEnumerable<double> SnowFraction,
    [property: JsonPropertyName("windspeed")] IEnumerable<double> WindSpeed,
    [property: JsonPropertyName("precipitation_probability")] IEnumerable<int> PrecipitationProbability,
    [property: JsonPropertyName("convective_precipitation")] IEnumerable<double> ConvectivePrecipitation,
    [property: JsonPropertyName("rainspot")] IEnumerable<string> RainSpot,
    [property: JsonPropertyName("pictocode")] IEnumerable<int> PicToCode,
    [property: JsonPropertyName("felttemperature")] IEnumerable<double> FeltTemperature,
    [property: JsonPropertyName("precipitation")] IEnumerable<double> Precipitation,
    [property: JsonPropertyName("isdaylight")] IEnumerable<int> IsDayLight,
    [property: JsonPropertyName("uvindex")] IEnumerable<int> UvIndex,
    [property: JsonPropertyName("relativehumidity")] IEnumerable<int> RelativeHumidity,
    [property: JsonPropertyName("sealevelpressure")] IEnumerable<double> SeaLevelPressure,
    [property: JsonPropertyName("winddirection")] IEnumerable<int> WindDirection
);