using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_System_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseApiController
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthService _AuthService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuthService authService)
        {
            _logger = logger;
            _AuthService = authService; 
        }

        [HttpGet("GetWeatherForecast"), Authorize(Roles = "admin")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetEmplo"), Authorize(Roles = "employee"), Authorize(Roles = "Supervisor")]
        public IEnumerable<WeatherForecast> getEmplo()
        {
            var rng = new Random();
            return Enumerable.Range(8,9).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetSup"), Authorize(Roles = "Supervisor")]
        public IEnumerable<WeatherForecast> getSuper()
        {
            var rng = new Random();
            return Enumerable.Range(8, 9).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }



    }
}
