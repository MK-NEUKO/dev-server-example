using System.Text.Json.Serialization;

namespace WeatherForecastApi.WeatherForecast;

public record ForecastDataPerDay(
    [property: JsonPropertyName("time")] IEnumerable<string> Time,
    [property: JsonPropertyName("temperature_instant")] IEnumerable<double> TemperatureInstant,
    [property: JsonPropertyName("precipitation")] IEnumerable<double> Precipitation,
    [property: JsonPropertyName("predictability")] IEnumerable<int> Predictability,
    [property: JsonPropertyName("temperature_max")] IEnumerable<double> TemperatureMax,
    [property: JsonPropertyName("sealevelpressure_mean")] IEnumerable<int> SeaLevelPressureMean,
    [property: JsonPropertyName("windspeed_mean")] IEnumerable<double> WindSpeedMean,
    [property: JsonPropertyName("precipitation_hours")] IEnumerable<double> PrecipitationHours,
    [property: JsonPropertyName("sealevelpressure_min")] IEnumerable<int> SeaLevelPressureMin,
    [property: JsonPropertyName("pictocode")] IEnumerable<int> PictogramCode,
    [property: JsonPropertyName("snowfraction")] IEnumerable<double> SnowFraction,
    [property: JsonPropertyName("humiditygreater90_hours")] IEnumerable<double> HumidityGreater90Hours,
    [property: JsonPropertyName("convective_precipitation")] IEnumerable<double> ConvectivePrecipitation,
    [property: JsonPropertyName("relativehumidity_max")] IEnumerable<int> RelativeHumidityMax,
    [property: JsonPropertyName("temperature_min")] IEnumerable<double> TemperatureMin,
    [property: JsonPropertyName("winddirection")] IEnumerable<int> WindDirection,
    [property: JsonPropertyName("felttemperature_max")] IEnumerable<double> FeltTemperatureMax,
    [property: JsonPropertyName("indexto1hvalues_end")] IEnumerable<int> IndexTo1hValuesEnd,
    [property: JsonPropertyName("relativehumidity_min")] IEnumerable<int> RelativeHumidityMin,
    [property: JsonPropertyName("felttemperature_mean")] IEnumerable<double> FeltTemperatureMean,
    [property: JsonPropertyName("windspeed_min")] IEnumerable<double> WindSpeedMin,
    [property: JsonPropertyName("felttemperature_min")] IEnumerable<double> FeltTemperatureMin,
    [property: JsonPropertyName("precipitation_probability")] IEnumerable<int> PrecipitationProbability,
    [property: JsonPropertyName("uvindex")] IEnumerable<int> UvIndex,
    [property: JsonPropertyName("indexto1hvalues_start")] IEnumerable<int> IndexTo1hValuesStart,
    [property: JsonPropertyName("rainspot")] IEnumerable<string> RainSpot,
    [property: JsonPropertyName("temperature_mean")] IEnumerable<double> TemperatureMean,
    [property: JsonPropertyName("sealevelpressure_max")] IEnumerable<int> SeaLevelPressureMax,
    [property: JsonPropertyName("relativehumidity_mean")] IEnumerable<int> RelativeHumidityMean,
    [property: JsonPropertyName("predictability_class")] IEnumerable<double> PredictabilityClass,
    [property: JsonPropertyName("windspeed_max")] IEnumerable<double> WindSpeedMax
);