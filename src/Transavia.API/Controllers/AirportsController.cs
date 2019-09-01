using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transavia.API.ViewModels;
using Transavia.Application.Commands.AddAirport;
using Transavia.Application.Queries.GetAirports;
using Transavia.Application.Queries.GetSupportedAirportSizes;
using Transavia.Application.Queries.GetSupportedAirportStatuses;
using Transavia.Application.Queries.GetSupportedAirportTypes;

namespace Transavia.API.Controllers
{
    [ApiController]
    [Route("api/airports")]
    public sealed class AirportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AirportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? country, int skipCount, int takeCount)
        {
            if (skipCount < 0 || takeCount < 0)
            {
                return BadRequest($"{nameof(skipCount)} and {nameof(takeCount)} must be greater or equal to 0.");
            }

            var result = await _mediator.Send(new GetAirportsQuery
            {
                CountryId = country,
                SkipCount = skipCount,
                TakeCount = takeCount
            });

            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<IEnumerable<AirportType>> GetSupportedAirportTypes()
        {
            return await _mediator.Send(new GetSupportedAirportTypesQuery());
        }

        [HttpGet("sizes")]
        public async Task<IEnumerable<Size>> GetSupportedSizes()
        {
            return await _mediator.Send(new GetSupportedAirportSizesQuery());
        }


        [HttpGet("statuses")]
        public async Task<IEnumerable<Status>> GetSupportedStatuses()
        {
            return await _mediator.Send(new GetSupportedAirportStatusesQuery());
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
