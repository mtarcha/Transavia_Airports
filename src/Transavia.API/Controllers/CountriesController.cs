using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transavia.API.ViewModels;
using Transavia.Application.Commands.AddAirport;
using Transavia.Application.Queries.GetAirports;
using Transavia.Application.Queries.GetSupportedCountries;

namespace Transavia.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> GetSupportedCountries()
        {
            return await _mediator.Send(new GetSupportedCountriesQuery());
        }
    }
}
