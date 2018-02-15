using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForcast.Web.Models;
using BOA.WeatherForcast.Web.ViewModels;

namespace BOA.WeatherForcast.Web.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityViewModel>().ReverseMap();
        }
    }
}
