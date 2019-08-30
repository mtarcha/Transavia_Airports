using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transavia.API.ViewModels;
using Transavia.Application.Commands.AddAirport;
using Transavia.Application.Queries.GetAirports;

namespace Transavia.API.Controllers
{
    [ApiController]
    [Route("api/airports")]
    public class AirportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AirportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
        public async Task<IActionResult> AddAirport(AddAirportViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<AddAirportCommand>(viewModel);
            var result = await _mediator.Send(command);

            return Ok(result.CreatedAirportId);
        }
    }
}
