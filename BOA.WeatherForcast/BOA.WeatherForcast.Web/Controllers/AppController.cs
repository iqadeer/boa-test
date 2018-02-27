using BOA.WeatherForecast.Util;
using Microsoft.AspNetCore.Mvc;

namespace BOA.WeatherForcast.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IWeatherAppLogger<AppController> _logger;

        public AppController(IWeatherAppLogger<AppController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string country)
        {
                //var cities = _repository.Get(country ?? "GB");
                //var selectList = new List<SelectListItem>();
                //foreach (var item in cities)
                //{
                //    selectList.Add(new SelectListItem{Text = item.Name, Value = item.Id});
                //}

                //var vm = new CityViewModel
                //{
                //    ListofCities = selectList
                //};

                return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Index(CityViewModel model)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    return RedirectToAction("Index");
        //    //}
        //    return View();
        //}
    }
}