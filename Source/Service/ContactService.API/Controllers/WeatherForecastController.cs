using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContactService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        {

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return "hello";
        }
    }
}
