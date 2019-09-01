using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedCountries
{
    public sealed class GetSupportedCountriesQuery : IRequest<IEnumerable<Country>>
    {
    }
}