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
    private readonly Dictionary<string, IWeatherService> _clients = new Dictionary<string, IWeatherService>();

    public void Register(string name, IWeatherService client)
    {
        _clients[name] = client;
    }

    public IWeatherService Resolve(string name)
    {
        return _clients[name];
    }
}
}
