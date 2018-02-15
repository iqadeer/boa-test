using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForcast.Web.ViewModels;
using BOA.WeatherForecast.Data;
using BOA.WeatherForecast.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BOA.WeatherForcast.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ForecastController : Controller
    {
        private readonly ILogger<ForecastController> _logger;
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public ForecastController(ILogger<ForecastController> logger, IRepository<City> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var cities = _repository.Get();
                return Ok(_mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(cities));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting cities for the file, Error: {ex.Message} Exception type: {ex.GetType()}.");
                return BadRequest("Failed to get list of supported cities.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var path =
                        $"http://api.openweathermap.org/data/2.5/forecast?id={id}&units=metric&appid=d958826a9d8f775f24dbeddcf2838369";
                    var response = await client.GetAsync(path);
                    response.EnsureSuccessStatusCode();
                    var contentString  = await response.Content.ReadAsStringAsync();
                    return Ok(value: JsonConvert.DeserializeObject<WeatherForecast.Domain.WeatherForecast>(contentString));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting cities for the file, Error: {ex.Message} Exception type: {ex.GetType()}.");
                return BadRequest("Failed to get list of supported cities.");
            }
        }


    }
}