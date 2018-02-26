using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BOA.WeatherForecast.Data.Entities;
using BOA.WeatherForecast.Util;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BOA.WeatherForecast.Data
{
    public class CityRepository : IRepository<City>
    {
        private readonly IWeatherAppLogger<CityRepository> _logger;

        public CityRepository(IWeatherAppLogger<CityRepository> logger)
        {
            _logger = logger;
        }
        public IEnumerable<City> Get(string country = "GB")
        {
            try
            {
//                return WriteShortListedFileAndReturnShortListOfCities(country);
                return ShortListedCities(country);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading cities from the file: Error : {ex.Message} Exception type: {ex.GetType()}");

                return null;
            }
        }

        private IEnumerable<City> WriteShortListedFileAndReturnShortListOfCities(string country)
        {
            using (var r = new StreamReader("Data/city.list.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<City>>(json);

                var shortListOfCities = items.Where(c => c.Country == country).Distinct().Take(5).ToList();
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "london"));
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "reading"));
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "manchester"));
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "birmingham"));
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "bristol"));
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "leeds"));
                shortListOfCities.Add(items.FirstOrDefault(c => c.Country == country && c.Name.ToLowerInvariant() == "oxford"));

                var serializedShortList = JsonConvert.SerializeObject(shortListOfCities);
                File.WriteAllText("Data/short.city.list.json", serializedShortList);

                return ShortListedCities(country);
            }
        }

        private IEnumerable<City> ShortListedCities(string country)
        {
            using (var r = new StreamReader("Data/short.city.list.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<City>>(json);
                return items.Where(c => c.Country == country).Distinct().ToList();
            }
        }

    }
}