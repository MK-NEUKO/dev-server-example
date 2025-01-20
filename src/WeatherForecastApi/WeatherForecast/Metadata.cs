using System.Text.Json.Serialization;

namespace WeatherForecastApi.WeatherForecast;

public record Metadata(
    [property: JsonPropertyName("modelrun_updatetime_utc")] DateTime ModelRunUpdateTimeUtc,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("height")] int Height,
    [property: JsonPropertyName("timezone_abbrevation")] string TimezoneAbbrevation,
    [property: JsonPropertyName("latitude")] double Latidude,
    [property: JsonPropertyName("modelrun_utc")] DateTime ModelRunUtc,
    [property: JsonPropertyName("longitude")] double Longitude,
    [property: JsonPropertyName("utc_timeoffset")] double UtcTimeoffset,
    [property: JsonPropertyName("generation_time_ms")] double GenerationTimeMs
);