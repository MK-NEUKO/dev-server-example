namespace WeatherForecastApi.Services.LocationService;

public sealed record LocationDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string CountryCodeIso2 { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public double Lat { get; init; }
    public double Lon { get; init; }
    public int AboveSeaLevel { get; init; }
    public string Timezone { get; init; }
    public int Population { get; init; }
    public double Distance { get; init; }
    public string IcaoCode { get; init; }
    public string IataCode { get; init; }
    public IEnumerable<string> Postcodes { get; init; }
    public string FeatureClass { get; init; }
    public string FeatureCode { get; init; }
    public string MeteoBlueUrl { get; init; }
}