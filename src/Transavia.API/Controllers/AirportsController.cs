using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transavia.Application.Queries.GetAirports;

namespace Transavia.API.Controllers
{
    [Route("api/airports")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AirportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Airport>> Get(string countryCode = null, int skipCount = 0, int takeCount = 8)
        {
            return await _mediator.Send(new GetAirportsQuery());
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
