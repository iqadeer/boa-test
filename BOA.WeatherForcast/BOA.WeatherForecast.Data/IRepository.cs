using System.Collections.Generic;

namespace BOA.WeatherForecast.Data
{
    public interface IRepository<out T> 
    {
        IEnumerable<T> Get(string country);

    }
}
