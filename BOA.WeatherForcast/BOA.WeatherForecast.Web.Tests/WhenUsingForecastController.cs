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
        public void when_calling_GetCities_should_return_all_cities()
        {
            var result = _forecastController.Get();

            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;

            Assert.NotNull(objectResult);

            Assert.NotNull(objectResult.Value);
            Assert.IsInstanceOf<IEnumerable<CityViewModel>>(objectResult.Value);
        }
        
    }
}