using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForecast.Api.ViewModels;
using BOA.WeatherForecast.Data;
using BOA.WeatherForecast.Data.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BOA.WeatherForecast.Api.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpClientWrapper _client;
        private readonly IConfiguration _configuration;

        public ForecastService(IRepository<City> repository, IMapper mapper, IHttpClientWrapper client, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
            _configuration = configuration;
        }

        public IEnumerable<CityViewModel> GetCities(string country)
        {
                return _mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(_repository.Get(country));
        }

        public async Task<Domain.WeatherForecast> GetWeatherForecast(int id)
        {
            var baseUrl = _configuration.GetValue<string>("WeatherServiceBaseUrl"); 
            var apiKey = _configuration.GetValue<string>("AppId"); 
            var units = _configuration.GetValue<string>("Units");
            var clientUrl = $"{baseUrl}?id={id}&units={units}&appid={apiKey}";
                var response = await _client.GetAsync(clientUrl);
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

