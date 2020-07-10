using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DepInjTwo.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DepInjTwo.Services
{
    public class OhioWeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        public OhioWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCurrentTemp()
        {
            return "Ohio Weather " + _configuration["WeatherValue"];
        }
    }    
}
