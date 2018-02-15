using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BOA.WeatherForecast.Data.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BOA.WeatherForecast.Data
{
    public class CityRepository : IRepository<City>
    {
        private readonly ILogger<CityRepository> _logger;

        public CityRepository(ILogger<CityRepository> logger)
        {
            _logger = logger;
        }
        public IEnumerable<City> Get()
        {
            try
            {
                using (var r = new StreamReader("Data/city.list.json"))
                {
                    var json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject<List<City>>(json);
                    return items.Where(c => c.Country == "GB").Distinct().Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading cities from the file: Error : {ex.Message} Exception type: {ex.GetType()}");

                return null;
            }
        }
    }
}