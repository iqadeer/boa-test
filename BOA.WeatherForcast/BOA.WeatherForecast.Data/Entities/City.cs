namespace BOA.WeatherForecast.Data.Entities
{
    public class City
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public Coord Coord { get; set; }
        }
}
