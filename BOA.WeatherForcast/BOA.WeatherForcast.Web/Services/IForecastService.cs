﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BOA.WeatherForcast.Web.ViewModels;

namespace BOA.WeatherForcast.Web.Services
{
    public interface IForecastService : IDisposable
    {
        IEnumerable<CityViewModel> GetCities(string country);
        Task<WeatherForecast.Domain.WeatherForecast> GetWeatherForecast(int id);
    }
}