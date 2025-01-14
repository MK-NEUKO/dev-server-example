using System.Text.Json.Serialization;

namespace WeatherForecastApi.Domain.Location;

public record LocationQueryResult
{
    [JsonConstructor]
    public LocationQueryResult(
        string query,
        string? iso2,
        int currentPage,
        int itemsPerPage,
        int pages,
        int count,
        string orderBy,
        double? lat,
        double? lon,
        int radius,
        string type,
        IEnumerable<Location> results
        )
    {
        Query = query;
        Iso2 = iso2;
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        Pages = pages;
        Count = count;
        OrderBy = orderBy;
        Lat = lat;
        Lon = lon;
        Radius = radius;
        Type = type;
        Results = results;
    }

    [JsonPropertyName("query")]
    public string Query { get; init; } = string.Empty;

    [JsonPropertyName("iso2")]
    public string? Iso2 { get; init; }

    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; init; }

    [JsonPropertyName("itemsPerPage")]
    public int ItemsPerPage { get; init; }

    [JsonPropertyName("pages")]
    public int Pages { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("orderBy")]
    public string OrderBy { get; init; } = string.Empty;

    [JsonPropertyName("lat")]
    public double? Lat { get; init; }

    [JsonPropertyName("lon")]
    public double? Lon { get; init; }

    [JsonPropertyName("radius")]
    public int Radius { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("results")]
    public IEnumerable<Location> Results { get; init; } = new List<Location>();
}