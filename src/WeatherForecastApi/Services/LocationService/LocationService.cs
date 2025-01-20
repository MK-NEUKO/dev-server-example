using WeatherForecastApi.Location;
using WeatherForecastApi.Services.Abstractions;

namespace WeatherForecastApi.Services.LocationService;

public class LocationService(
    ILocationRepository _locationRepository
    ) : ILocationService
{
    public async Task<LocationQueryResultDto> HandleAsync(string query, CancellationToken cancellationToken)
    {
        // get data from repository
        var result = await _locationRepository.GetLocationsAsync(query, cancellationToken);
        // map to LocationQueryResultDto
        var dto = new LocationQueryResultDto
        {
            Query = result.Query,
            Iso2 = result.Iso2,
            CurrentPage = result.CurrentPage,
            ItemsPerPage = result.ItemsPerPage,
            Pages = result.Pages,
            Count = result.Count,
            OrderBy = result.OrderBy,
            Lat = result.Lat,
            Lon = result.Lon,
            Radius = result.Radius,
            Type = result.Type,
            Results = result.Results.Select(x => new LocationDto
            {
                Id = x.Id,
                Name = x.Name,
                CountryCodeIso2 = x.CountryCodeIso2,
                Country = x.Country,
                State = x.State,
                Lat = x.Lat,
                Lon = x.Lon,
                AboveSeaLevel = x.AboveSeaLevel,
                Timezone = x.Timezone,
                Population = x.Population,
                Distance = x.Distance,
                IcaoCode = x.IcaoCode,
                IataCode = x.IataCode,
                Postcodes = x.Postcodes,
                FeatureClass = x.FeatureClass,
                FeatureCode = x.FeatureCode,
                MeteoBlueUrl = x.MeteoBlueUrl
            })
        };
        // return
        return dto;
    }
}