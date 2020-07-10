// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using DepInjTwo.Interfaces;
// using Microsoft.Extensions.Configuration;
// using Xunit;
// using DepInjTwo.Factories;
// using DepInjTwo.Services;
// using DepInjTwo.Controllers;
// using Moq;
// using Newtonsoft.Json;

// namespace DepInjTwo.tests
// {
//     public class SimpleTests
//     {
//         private IWeatherFactory _factory;
//         private IWeatherService _realOhioService;
//         private IWeatherService _realIndianaService;
//         private Mock<ILogger<WeatherForecastController>> _logger;
//         private Mock<IWeatherFactory> _mockFactory;
//         private Mock<IWeatherService> _mockOhioService;
//         private Mock<IWeatherService> _mockIndianaService;

//         public SimpleTests()
//         {   
//             var configItems = new List<KeyValuePair<string, string>>();
//             configItems.Add(new KeyValuePair<string, string>("WeatherValue", "this is coming from a Config"));
//             var builder = new ConfigurationBuilder();
//             builder.AddInMemoryCollection(configItems);
//             var config = builder.Build();

//             _mockFactory = new Mock<IWeatherFactory>();
//             _mockOhioService = new Mock<IWeatherService>();
//             _mockIndianaService = new Mock<IWeatherService>();
//             _mockOhioService.Setup(c => c.GetCurrentTemp()).Returns("This is from the Ohio moq");
//             _mockIndianaService.Setup(c => c.GetCurrentTemp()).Returns("This is from the Indiana moq");
//             _mockFactory.Setup(c => c.Resolve("OhioWeatherService")).Returns(_mockOhioService.Object);
//             _mockFactory.Setup(c => c.Resolve("IndianaWeatherService")).Returns(_mockIndianaService.Object);


//             _factory = new WeatherFactory();
//             _factory.Register("OhioWeatherService", new OhioWeatherService(config));
//             _factory.Register("IndianaWeatherService", new IndianaWeatherService(config));
//             _logger = new Mock<ILogger<WeatherForecastController>>();
//         }

//         [Fact]
//         private void it_should_get_correct_info_for_both_states_with_real_service()
//         {
//             var controller = new WeatherForecastController(_logger.Object, _factory);
//             var results = controller.Get();
//             var IndianaResult = results.FirstOrDefault(c => c.Summary.Contains("Indiana"));
            
//             Assert.NotEmpty(results);
//             Assert.Equal(IndianaResult.Summary, "Indiana Weather this is coming from a Config");
//         }

//         [Fact]
//         private void it_should_get_correct_info_for_both_states_with_mock_service()
//         {
//             var controller = new WeatherForecastController(_logger.Object, _mockFactory.Object);
//             var results = controller.Get();
//             var ohio = results.FirstOrDefault(c => c.Summary == "This is from the Ohio moq");
//             var indiana = results.FirstOrDefault(c => c.Summary == "This is from the Indiana moq");            
//             Assert.NotNull(ohio);
//             Assert.NotNull(indiana);
//         }
//     }    
// }
