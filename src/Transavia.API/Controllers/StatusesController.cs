using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transavia.Application.Queries.GetSupportedAirportStatuses;

namespace Transavia.API.Controllers
{
    [ApiController]
    [Route("api/statuses")]
    public class StatusesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Status>> GetSupportedStatuses()
        {
            return await _mediator.Send(new GetSupportedAirportStatusesQuery());
        }
    }
}
