using System.Collections.Generic;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.ViewModels
{
    public class SearchAirportsViewModel
    {
        public int TotalFoundCount { get; set; }

        public IEnumerable<Airport> AirportsOnPage { get; set; }

        public PaginationViewModel Pagination { get; set; }

        public CountryViewModel SelectedCountry { get; set; }

        public IEnumerable<CountryViewModel> SupportedCountries { get; set; }
    }
}