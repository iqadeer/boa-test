using AutoMapper;
using BOA.WeatherForcast.Web.ViewModels;
using BOA.WeatherForecast.Data.Entities;

namespace BOA.WeatherForcast.Web.Data
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
