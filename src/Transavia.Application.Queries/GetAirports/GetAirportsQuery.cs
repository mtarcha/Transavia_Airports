using System;
using MediatR;

namespace Transavia.Application.Queries.GetAirports
{
    public class GetAirportsQuery : IRequest<GetAirportsResult>
    {
        public Guid? CountryId { get; set; }

        public int SkipCount { get; set; }

        public int TakeCount { get; set; }
    }
}