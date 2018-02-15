using System.Collections.Generic;
using System.IO;
using System.Linq;
using BOA.WeatherForcast.Web.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BOA.WeatherForcast.Web.Services
{
    public class CityRepository : IRepository<City>
    {
        public List<City> GetEntities()
        {
            using (StreamReader r = new StreamReader("Data/city.list.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<City>>(json);
                return items.Where(c => c.Country == "GB").Distinct().Take(10).ToList();
            }
        }
    }
}