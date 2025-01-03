using System.Text.Json.Serialization;

namespace WeatherForecastApi.Domain.Location;

public record Location( 
    [property: JsonPropertyName("name")] string Name = "",
    [property: JsonPropertyName("local_names")] Dictionary<string, string> LocalNames = null,
    [property: JsonPropertyName("lat")] double Lat = 0.0,
    [property: JsonPropertyName("lon")] double Lon = 0.0,
    [property: JsonPropertyName("country")] string Country = "",
    [property: JsonPropertyName("state")] string State = ""
    );