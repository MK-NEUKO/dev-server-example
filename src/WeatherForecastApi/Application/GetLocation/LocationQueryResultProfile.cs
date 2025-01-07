using AutoMapper;
using WeatherForecastApi.Domain.Location;

namespace WeatherForecastApi.Application.GetLocation;

public class LocationQueryResultProfile : Profile
{
    public LocationQueryResultProfile()
    {
        CreateMap<Location, LocationQueryResultDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.Lon))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
    }
}