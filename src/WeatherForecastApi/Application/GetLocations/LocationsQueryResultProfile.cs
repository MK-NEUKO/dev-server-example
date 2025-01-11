using AutoMapper;
using WeatherForecastApi.Domain.Location;

namespace WeatherForecastApi.Application.GetLocations;

public class LocationsQueryResultProfile : Profile
{
    public LocationsQueryResultProfile()
    {
        CreateMap<Location, LocationsQueryResultDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.Lon))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
    }
}