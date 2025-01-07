using System.Text.Json.Serialization;

namespace WeatherForecastApi.Domain.Location;

public record Location(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("iso2")] string CountryCodeIso2,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("admin1")] string State,
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonPropertyName("lon")] double Lon,
    [property: JsonPropertyName("asl")] int AboveSeaLevel,
    [property: JsonPropertyName("timezone")] string Timezone,
    [property: JsonPropertyName("population")] int Population,
    [property: JsonPropertyName("distance")] double Distance,
    [property: JsonPropertyName("icao")]  string IcaoCode,
    [property: JsonPropertyName("iata")] string IataCode,
    [property: JsonPropertyName("postcodes")] IEnumerable<string> Postcodes,
    [property: JsonPropertyName("featureClass")] string FeatureClass,
    [property: JsonPropertyName("featureCode")] string FeatureCode,
    [property: JsonPropertyName("url")] string MeteoBlueUrl,
    [property: JsonPropertyName("id")] int Id
    );