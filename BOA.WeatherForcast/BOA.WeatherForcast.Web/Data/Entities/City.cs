using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOA.WeatherForcast.Web.Models
{
    public class City
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public Coord Coord { get; set; }
        }
}
