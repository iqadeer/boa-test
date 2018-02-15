using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForcast.Web.Models;
using BOA.WeatherForcast.Web.Services;
using BOA.WeatherForcast.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            //throw new InvalidOperationException();
            using (StreamReader r = new StreamReader("Data/city.list.json"))
            {
                var cities = _repository.GetEntities();
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