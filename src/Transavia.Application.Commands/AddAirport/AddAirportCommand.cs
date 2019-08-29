using System;
using MediatR;

namespace Transavia.Application.Commands.AddAirport
{
    public class AddAirportCommand : IRequest<AddAirportResult>
    {
        public string Iata { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }

        public Guid TypeId { get; set; }

        public Guid SizeId { get; set; }

        public Guid StatusId { get; set; }

        public string Lon { get; set; }

        public string Lat { get; set; }
    }
}