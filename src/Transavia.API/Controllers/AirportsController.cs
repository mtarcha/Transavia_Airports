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
        public async Task<GetAirportsResult> Get(Guid? country, int skipCount, int takeCount)
        {
            return await _mediator.Send(new GetAirportsQuery
            {
                CountryId = country,
                SkipCount = skipCount,
                TakeCount = takeCount
            });
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
