using System.Collections.Generic;

namespace BOA.WeatherForecast.Domain
{
    public class WeatherForecast
    {
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }
        public List<List> List { get; set; }
        public City City { get; set; }
    }
}
