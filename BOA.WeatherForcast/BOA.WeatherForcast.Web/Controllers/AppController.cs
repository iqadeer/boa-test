using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForcast.Web.Models;
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

        public AppController(IMapper mapper, ILogger<AppController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //throw new InvalidOperationException();
            using (StreamReader r = new StreamReader("Data/city.list.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<City>>(json);
                var ukCities = items.Where(c => c.Country == "GB").Distinct().Take(10).ToList();


                //var cities = _mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(ukCities);
                var selectList = new List<SelectListItem>();
                foreach (var item in ukCities)
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