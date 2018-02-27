using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BOA.WeatherForecast.Api.Controllers;
using BOA.WeatherForecast.Api.Services;
using BOA.WeatherForecast.Api.ViewModels;
using BOA.WeatherForecast.Util;
using Microsoft.AspNetCore.Mvc;
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
            foreCastService.Setup(m => m.GetWeatherForecast(It.IsAny<int>()))
                .Returns(Task.FromResult(new Domain.WeatherForecast()));

            var logger = new Mock<IWeatherAppLogger<ForecastController>>();
            logger.Setup(m => m.LogError(It.IsAny<string>(), "abcd")).Verifiable();

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

        [Test]
        public void when_calling_GetCities_should_return_bad_request_object_if_exception_is_thrown()
        {
            var foreCastService = new Mock<IForecastService>();
            foreCastService.Setup(m => m.GetCities(It.IsAny<string>()))
                .Throws<Exception>();
            var logger = new Mock<IWeatherAppLogger<ForecastController>>();
            _forecastController = new ForecastController(logger.Object, foreCastService.Object);
            var result = _forecastController.Get();
            var objectResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(objectResult);
        }

        [Test]
        public void when_calling_GetCities_error_is_logged_when_exception_happened()
        {
            var foreCastService = new Mock<IForecastService>();
            foreCastService.Setup(m => m.GetCities(It.IsAny<string>()))
                 .Throws<Exception>();

            var logger = new Mock<IWeatherAppLogger<ForecastController>>();
            logger.Setup(m => m.LogError(It.IsAny<string>())).Verifiable();

            _forecastController = new ForecastController(logger.Object, foreCastService.Object);
            var result = _forecastController.Get();

            logger.Verify(m => m.LogError(It.IsAny<string>()), Times.Once);
        }
        [Test]




        public void when_calling_Get_int_should_return_weatherforecast_if_serivce_return_ok()
        {
            var result = _forecastController.Get(2);
            var objectResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<Domain.WeatherForecast>(objectResult.Value);
        }
        [Test]
        public void when_calling_Get_int_should_not_return_null()
        {
            var result = _forecastController.Get(2);

            Assert.NotNull(result);

        }
        [Test]
        public void when_calling_Get_int_should_return_OkObjectResult_if_proxy_returns_data()
        {
            var result = _forecastController.Get(2);
            var objectResult = result.Result as OkObjectResult;
            Assert.NotNull(objectResult);
        }
        [Test]
        public void when_calling_Get_int_should_return_bad_request_object_if_exception_is_thrown()
        {
            var foreCastService = new Mock<IForecastService>();
            foreCastService.Setup(m => m.GetWeatherForecast(It.IsAny<int>()))
                .Throws<Exception>();
            var logger = new Mock<IWeatherAppLogger<ForecastController>>();
            _forecastController = new ForecastController(logger.Object, foreCastService.Object);
            var result = _forecastController.Get(2);
            var objectResult = result.Result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(objectResult);
        }

        [Test]
        public void when_calling_Get_int_error_is_logged_when_exception_happened()
        {
            var foreCastService = new Mock<IForecastService>();
            foreCastService.Setup(m => m.GetWeatherForecast(It.IsAny<int>()))
                 .Throws<Exception>();

            var logger = new Mock<IWeatherAppLogger<ForecastController>>();
            logger.Setup(m => m.LogError(It.IsAny<string>())).Verifiable();

            _forecastController = new ForecastController(logger.Object, foreCastService.Object);
            var result = _forecastController.Get(2);

            logger.Verify(m => m.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}