using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Transavia.API.Controllers
{
    [Route("api/airports")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromQuery] string countryCode = null)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
