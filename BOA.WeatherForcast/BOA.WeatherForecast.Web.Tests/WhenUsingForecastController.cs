using System.Collections.Generic;
using BOA.WeatherForcast.Web.Controllers;
using BOA.WeatherForcast.Web.Services;
using BOA.WeatherForcast.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BOA.WeatherForecast.Web.Tests
{
    [TestFixture]
    public class WhenUsingForecastController
    {
        private ForecastController _forecastController;

        [SetUp]
        public void SetUp()
        {
            var foreCastService = new Mock<IForecastService>();

            foreCastService.Setup(m => m.GetCities(It.IsAny<string>()))
                .Returns(new List<CityViewModel>());

            var logger = new Mock<ILogger<ForecastController>>();

            _forecastController = new ForecastController(logger.Object, foreCastService.Object);
        }

        [Test]
        public void when_calling_GetCities_should_return_list_of_cites_if_serivce_return_ok()
        {
            var result = _forecastController.Get();
            var objectResult = result as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<CityViewModel>>(objectResult.Value);
        }
        [Test]
        public void when_calling_GetCities_should_not_return_null()
        {
            var result = _forecastController.Get();

            Assert.NotNull(result);

        }
        [Test]
        public void when_calling_GetCities_should_return_OkObjectResult_if_proxy_returns_data()
        {
            var result = _forecastController.Get();

            var objectResult = result as OkObjectResult;

            Assert.NotNull(objectResult);

        }

    }
}