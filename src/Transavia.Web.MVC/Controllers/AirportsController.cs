using System;
using Microsoft.AspNetCore.Mvc;
using Transavia.Web.MVC.Clients;
using Transavia.Web.MVC.ViewModels;

namespace Transavia.Web.MVC.Controllers
{
    [Controller]
    public class AirportsController : Controller
    {
        private const int AirportsOnPage = 30;

        private readonly IAirportsClient _client;

        public AirportsController(IAirportsClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Get(Guid? country = null, int page = 1)
        {
            if (page < 1)
            {
                return BadRequest($"Invalid page '{page}'! Should be 1 or more.");
            }

            var airportsResult = _client.Get(country, (page - 1) * AirportsOnPage, AirportsOnPage).Result.GetContent();

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

            var vm = new AirportsViewModel
            {
                TotalFoundCount = airportsResult.TotalFound,
                AirportsOnPage = airportsResult.Airports,
                Pagination = new PaginationViewModel(totalPages, page),
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