using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transavia.Web.MVC.Clients;
using Transavia.Web.MVC.ViewModels;

namespace Transavia.Web.MVC.Controllers
{
    [Controller]
    public class AirportsController : Controller
    {
        private readonly IAirportsClient _client;

        public AirportsController(IAirportsClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Get(string country, int page)
        {
            var airports = _client.Get("", 0, 8);

            var vm = new AirportsViewModel
            {
                TotalAirportsCount = 100,
                AirportsOnPage = airports.Result.GetContent(),
                Pagination = new PaginationViewModel(100, page),
                Country = country
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