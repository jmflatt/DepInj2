using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DepInjTwo.Factories;
using DepInjTwo.Services;
using DepInjTwo.Interfaces;

namespace DepInjTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private IWeatherFactory _weatherFactory;
        private IWeatherService _ohioWeatherService;
        private IWeatherService _indianaWeatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherFactory weatherFactory)
        {
            _logger = logger;

            _weatherFactory = weatherFactory;
            _ohioWeatherService = _weatherFactory.GetWeatherProvider<OhioWeatherService>();
            _indianaWeatherService = _weatherFactory.GetWeatherProvider<IndianaWeatherService>();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var ohioSummary = _ohioWeatherService.GetCurrentTemp();
            var indianaSummary = _indianaWeatherService.GetCurrentTemp();

            return new List<WeatherForecast>() 
            {
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = 22,
                    Summary = ohioSummary
                },
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = 21,
                    Summary = indianaSummary
                }
            }
            .ToArray();
        }
    }
}
