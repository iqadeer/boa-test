using System;
using System.Threading.Tasks;
using BOA.WeatherForecast.Api.Services;
using BOA.WeatherForecast.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BOA.WeatherForecast.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [EnableCors("dev")]
    public class ForecastController : Controller
    {
        private readonly IWeatherAppLogger<ForecastController> _logger;
        private readonly IForecastService _forecastService;

        public ForecastController(IWeatherAppLogger<ForecastController> logger, IForecastService forecastService)
        {
            _logger = logger;
            _forecastService = forecastService;
        }

        [HttpGet]
        public IActionResult Get(string country = "GB")
        {
            try
            {
                return Ok(_forecastService.GetCities(country));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to get list of supported cities.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _forecastService.GetWeatherForecast(id));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to get list of supported cities.");
            }
        }

        private void LogError(Exception ex)
        {
            _logger.LogError($"Error getting cities for the file, Error: {ex.Message} Exception type: {ex.GetType()}.");
        }
    }
}