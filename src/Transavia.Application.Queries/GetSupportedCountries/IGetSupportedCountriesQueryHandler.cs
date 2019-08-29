using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedCountries
{
    public interface IGetSupportedAirportCountriesQueryHandler : IRequestHandler<GetSupportedCountriesQuery, IEnumerable<Country>>
    {
    }
}