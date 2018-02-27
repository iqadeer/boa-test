using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BOA.WeatherForecast.Api.ViewModels;

namespace BOA.WeatherForecast.Api.Services
{
    public interface IForecastService : IDisposable
    {
        IEnumerable<CityViewModel> GetCities(string country);
        Task<WeatherForecast.Domain.WeatherForecast> GetWeatherForecast(int id);
    }
}