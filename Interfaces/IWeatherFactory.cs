using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DepInjTwo.Interfaces
{
    public interface IWeatherFactory
    {
        void Register(string name, IWeatherService service);
        IWeatherService Resolve(string name);
    }    
}
