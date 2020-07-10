using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DepInjTwo.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using DepInjTwo.models;

namespace DepInjTwo.Services
{
    public class OhioWeatherService : IWeatherService
    {
        private readonly IOptionsMonitor<WeatherOptions> _weatherOptions;
        public OhioWeatherService(IOptionsMonitor<WeatherOptions> options)
        {
            _weatherOptions = options;
        }

        public string GetCurrentTemp()
        {
            return "Ohio Weather " + _weatherOptions.CurrentValue.WeatherValue;
        }
    }    
}
