using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.ViewModels
{
    public class AddAirportViewModel
    {
        [Required]
        public string Iata { get; set; }

        public string Lon { get; set; }

        public string Lat { get; set; }

        public string Name { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        [Required]
        public Guid CountryId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        public Guid SizeId { get; set; }

        public IEnumerable<Status> SupportedStatuses { get; set; }

        public IEnumerable<CountryViewModel> SupportedCountries { get; set; }

        public IEnumerable<AirportType> SupportedTypes { get; set; }

        public IEnumerable<Size> SupportedSizes { get; set; }
    }
}