using System;
using Microsoft.Extensions.Logging;

namespace BOA.WeatherForecast.Util
{
    public class WeatherAppLogger<T> : IWeatherAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public WeatherAppLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(string message, params object[] arg)
        {
            _logger.LogError(message, arg);
        }
    }
}
