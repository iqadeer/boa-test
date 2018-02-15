using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BOA.WeatherForcast.Web.Services
{
    public interface IRepository<T> 
    {
        List<T> GetEntities();

    }
}
