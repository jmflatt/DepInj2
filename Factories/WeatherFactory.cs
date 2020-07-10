using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DepInjTwo.Interfaces;
using DepInjTwo.Services;

namespace DepInjTwo.Factories
{
    class WeatherFactory : IWeatherFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public WeatherFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IWeatherService GetWeatherProvider<T>() where T : IWeatherService
        {
            return (IWeatherService)_serviceProvider.GetService(typeof(T));
        }
    }
}




