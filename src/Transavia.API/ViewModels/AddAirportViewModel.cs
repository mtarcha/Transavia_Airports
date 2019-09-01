using System;
using System.ComponentModel.DataAnnotations;

namespace Transavia.API.ViewModels
{
    public sealed class AddAirportViewModel
    {
        [Required]
        public string Iata { get; set; }

        public string Name { get; set; }

        [Required]
        public Guid CountryId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        public Guid SizeId { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        public string Lon { get; set; }

        public string Lat { get; set; }
    }
}