﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForcast.Web.ViewModels;
using BOA.WeatherForecast.Data;
using BOA.WeatherForecast.Data.Entities;
using Newtonsoft.Json;

namespace BOA.WeatherForcast.Web.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpClientWrapper _client;

        public ForecastService(IRepository<City> repository, IMapper mapper, IHttpClientWrapper client)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
        }

        public IEnumerable<CityViewModel> GetCities(string country)
        {
                return _mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(_repository.Get(country));
        }

        public async Task<WeatherForecast.Domain.WeatherForecast> GetWeatherForecast(int id)
        {
                var path =
                    $"http://api.openweathermap.org/data/2.5/forecast?id={id}&units=metric&appid=d958826a9d8f775f24dbeddcf2838369";
                var response = await _client.GetAsync(path);
                response.EnsureSuccessStatusCode();
                var contentString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherForecast.Domain.WeatherForecast>(contentString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
            }
        }
    }
}

