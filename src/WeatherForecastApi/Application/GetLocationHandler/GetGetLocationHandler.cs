using WeatherForecastApi.Application.Abstractions;
using WeatherForecastApi.Location;

namespace WeatherForecastApi.Application.GetLocationHandler;

internal sealed class GetGetLocationHandler(
    ILocationRepository locationRepository
    ) : IGetLocationHandler
{
    public async Task<LocationQueryResultDto> HandleAsync(string query, CancellationToken cancellationToken)
    {
        var result = await locationRepository.GetLocationsAsync(query, cancellationToken);

        if (result == null)
        {
            throw new BadHttpRequestException("Invalid location query result.");
        }

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