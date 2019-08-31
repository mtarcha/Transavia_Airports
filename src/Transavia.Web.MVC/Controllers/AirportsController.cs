using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transavia.Web.MVC.Clients;
using Transavia.Web.MVC.Models;
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
        private readonly IStatusesClient _statusesClient;
        private readonly ITypesClient _typesClient;
        private readonly ISizesClient _sizesClient;

        public AirportsController(
            IMapper mapper, 
            IAirportsClient airportsClient, 
            ICountriesClient countriesClient,
            IStatusesClient statusesClient,
            ITypesClient typesClient,
            ISizesClient sizesClient)
        {
            _mapper = mapper;
            _airportsClient = airportsClient;
            _countriesClient = countriesClient;
            _statusesClient = statusesClient;
            _typesClient = typesClient;
            _sizesClient = sizesClient;
        }

        [HttpGet]
        public IActionResult Search(Guid? country = null, int page = 1)
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

        [HttpGet]
        public async  Task<IActionResult> AddAirport()
        {
            var countries = _mapper.Map<IEnumerable<CountryViewModel>>(_countriesClient.GetSupportedCountries().Result.GetContent());
            var statuses = await _statusesClient.GetSupportedStatuses();
            var sizes = await _sizesClient.GetSupportedSizes();
            var types = await _typesClient.GetSupportedTypes();

            var vm = new AddAirportViewModel
            {
                SupportedCountries = countries,
                SupportedStatuses = statuses.GetContent(),
                SupportedSizes = sizes.GetContent(),
                SupportedTypes = types.GetContent()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddAirport(AddAirportViewModel vm)
        {
            var model = _mapper.Map<AddAirportModel>(vm);
            var result = await _airportsClient.AddAirport(model);

            var id = result.GetContent();

            return RedirectToAction("Search");
        }
    }
}