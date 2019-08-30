using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transavia.Web.MVC.Clients;
using Transavia.Web.MVC.ViewModels;

namespace Transavia.Web.MVC.Controllers
{
    [Controller]
    public class AirportsController : Controller
    {
        private const int AirportsOnPage = 30;

        private readonly IMapper _mapper;
        private readonly IAirportsClient _airportsClient;
        private readonly ICountriesClient _countriesClient;

        public AirportsController(IMapper mapper, IAirportsClient airportsClient, ICountriesClient countriesClient)
        {
            _mapper = mapper;
            _airportsClient = airportsClient;
            _countriesClient = countriesClient;
        }

        [HttpGet]
        public IActionResult Get(Guid? country = null, int page = 1)
        {
            if (page < 1)
            {
                return BadRequest($"Invalid page '{page}'! Should be 1 or more.");
            }

            var airportsResult = _airportsClient.Get(country, (page - 1) * AirportsOnPage, AirportsOnPage).Result.GetContent();

            var totalPages = (int) Math.Ceiling((double) airportsResult.TotalFound / AirportsOnPage);

            foreach (var airport in airportsResult.Airports)
            {
                if (airport.Name == null)
                {
                    airport.Name = "<Unknown>";
                }

                if (airport.Status == "closed")
                {
                    airport.Type = "closed";
                }
            }

            var supportedCountries = _mapper.Map<IEnumerable<CountryViewModel>>(_countriesClient.GetSupportedCountries().Result.GetContent()).ToList();
            supportedCountries.Insert(0, new CountryViewModel {Name = "All"});

            var vm = new SearchAirportsViewModel
            {
                TotalFoundCount = airportsResult.TotalFound,
                AirportsOnPage = airportsResult.Airports,
                Pagination = new PaginationViewModel(totalPages, page),
                SelectedCountry = supportedCountries.Single(x => x.Id == country),
                SupportedCountries = supportedCountries
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return RedirectToAction("Get");
        }
    }
}