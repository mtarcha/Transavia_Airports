using System.Collections.Generic;

namespace Transavia.Web.MVC.ViewModels
{
    public class AddAirportViewModel
    {
        public IEnumerable<CountryViewModel> SupportedCountries { get; set; }
    }
}