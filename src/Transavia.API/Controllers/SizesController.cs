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
using Transavia.Application.Queries.GetSupportedCountries;

namespace Transavia.API.Controllers
{
    [ApiController]
    [Route("api/sizes")]
    public class SizesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SizesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Size>> GetSupportedSizes()
        {
            return await _mediator.Send(new GetSupportedAirportSizesQuery());
        }
    }
}
