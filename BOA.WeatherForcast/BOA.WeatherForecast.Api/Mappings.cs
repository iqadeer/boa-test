using AutoMapper;
using BOA.WeatherForecast.Api.ViewModels;
using BOA.WeatherForecast.Data.Entities;

namespace BOA.WeatherForecast.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityViewModel>()
                .ForMember(dt => dt.NameOfCity, src => src.MapFrom(o => o.Name)).ReverseMap();
        }
    }
}
