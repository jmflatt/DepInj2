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
using Microsoft.AspNetCore.Hosting;
using DepInjTwo.models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DepInjTwo.tests
{
    public class CustomAppFactory<TStartup>
    {

    }

    public class IntegrationTest
    {
        private IWeatherFactory _factory;
        private IWeatherService _realOhioService;
        private IWeatherService _realIndianaService;
        private Mock<ILogger<WeatherForecastController>> _logger = new Mock<ILogger<WeatherForecastController>>();

        public IntegrationTest()
        {

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = new ServiceCollection();
            services.Configure<WeatherOptions>(config.GetSection(WeatherOptions.Weather));

            services.AddScoped<IWeatherFactory, WeatherFactory>();

            services.AddScoped<OhioWeatherService>()
                .AddScoped<IWeatherService, OhioWeatherService>(s => s.GetService<OhioWeatherService>());

            services.AddScoped<IndianaWeatherService>()
                .AddScoped<IWeatherService, IndianaWeatherService>(s => s.GetService<IndianaWeatherService>());

            var serviceProvider = services.BuildServiceProvider();

            _factory = new WeatherFactory(serviceProvider);
        }

        [Fact]
        private void it_should_get_correct_info_for_both_states_with_real_service()
        {
            var controller = new WeatherForecastController(_logger.Object, _factory);
            var results = controller.Get();
            var IndianaResult = results.FirstOrDefault(c => c.Summary.Contains("Indiana"));

            Assert.NotEmpty(results);
            Assert.Equal(IndianaResult.Summary, "Indiana Weather this is coming from a Config");
        }
    }
}
