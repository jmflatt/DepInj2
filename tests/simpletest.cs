using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DepInjTwo.Interfaces;
using Microsoft.Extensions.Configuration;
using Xunit;
using DepInjTwo.Factories;
using DepInjTwo.Services;
using DepInjTwo.Controllers;
using Moq;
using Newtonsoft.Json;

namespace DepInjTwo.tests
{
    public class SimpleTests
    {
        private IWeatherFactory _factory;
        private IWeatherService _realOhioService;
        private IWeatherService _realIndianaService;
        private Mock<ILogger<WeatherForecastController>> _logger = new Mock<ILogger<WeatherForecastController>>();
        private Mock<IWeatherFactory> _mockFactory = new Mock<IWeatherFactory>();
        private Mock<IWeatherService> _mockOhioService = new Mock<IWeatherService>();
        private Mock<IWeatherService> _mockIndianaService = new Mock<IWeatherService>();
        private Mock<IServiceProvider> _mockServiceProvider = new Mock<IServiceProvider>();

        public SimpleTests()
        {   
            var configItems = new List<KeyValuePair<string, string>>();
            configItems.Add(new KeyValuePair<string, string>("WeatherValue", "this is coming from a Config"));
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(configItems);
            var config = builder.Build();
            
            _mockOhioService.Setup(c => c.GetCurrentTemp()).Returns("This is from the Ohio moq");
            _mockIndianaService.Setup(c => c.GetCurrentTemp()).Returns("This is from the Indiana moq");

            _mockFactory.Setup(c => c.GetWeatherProvider<OhioWeatherService>()).Returns(_mockOhioService.Object);
            _mockFactory.Setup(c => c.GetWeatherProvider<IndianaWeatherService>()).Returns(_mockIndianaService.Object);
        }

        [Fact]
        private void it_should_call_both_services_mock()
        {
            //just want to show how to mock the stuff. test doesnt actually test anything other than the mock is working
            var controller = new WeatherForecastController(_logger.Object, _mockFactory.Object);
            var results = controller.Get();
            var ohio = results.FirstOrDefault(c => c.Summary == "This is from the Ohio moq");
            var indiana = results.FirstOrDefault(c => c.Summary == "This is from the Indiana moq");            
            Assert.NotNull(ohio);
            Assert.NotNull(indiana);
        }
    }    
}
