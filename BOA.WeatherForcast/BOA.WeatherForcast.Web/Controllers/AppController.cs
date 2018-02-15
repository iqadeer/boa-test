using System.Collections.Generic;
using AutoMapper;
using BOA.WeatherForcast.Web.ViewModels;
using BOA.WeatherForecast.Data;
using BOA.WeatherForecast.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace BOA.WeatherForcast.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AppController> _logger;
        private readonly IRepository<City> _repository;

        public AppController(IMapper mapper, ILogger<AppController> logger, IRepository<City> repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
                var cities = _repository.Get();
                var selectList = new List<SelectListItem>();
                foreach (var item in cities)
                {
                    selectList.Add(new SelectListItem{Text = item.Name, Value = item.Id});
                }

                var vm = new CityViewModel
                {
                    ListofCities = selectList
                };

                return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}