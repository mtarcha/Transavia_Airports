using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transavia.Application.Queries.GetSupportedAirportTypes;

namespace Transavia.API.Controllers
{
    [ApiController]
    [Route("api/types")]
    public class AirportTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AirportTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<AirportType>> GetSupportedAirportTypes()
        {
            return await _mediator.Send(new GetSupportedAirportTypesQuery());
        }
    }
}
