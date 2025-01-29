using System.Text.Json.Serialization;

namespace WeatherForecastApi.WeatherForecast;

public record ForecastDataPerHour(
    [property: JsonPropertyName("time")] List<string> Time,
    [property: JsonPropertyName("snowfraction")] List<double> SnowFraction,
    [property: JsonPropertyName("windspeed")] List<double> WindSpeed,
    [property: JsonPropertyName("precipitation_probability")] List<int> PrecipitationProbability,
    [property: JsonPropertyName("convective_precipitation")] List<double> ConvectivePrecipitation,
    [property: JsonPropertyName("rainspot")] List<string> RainSpot,
    [property: JsonPropertyName("pictocode")] List<int> PicToCode,
    [property: JsonPropertyName("felttemperature")] List<double> FeltTemperature,
    [property: JsonPropertyName("precipitation")] List<double> Precipitation,
    [property: JsonPropertyName("isdaylight")] List<int> IsDayLight,
    [property: JsonPropertyName("uvindex")] List<int> UvIndex,
    [property: JsonPropertyName("relativehumidity")] List<int> RelativeHumidity,
    [property: JsonPropertyName("sealevelpressure")] List<double> SeaLevelPressure,
    [property: JsonPropertyName("winddirection")] List<int> WindDirection
);