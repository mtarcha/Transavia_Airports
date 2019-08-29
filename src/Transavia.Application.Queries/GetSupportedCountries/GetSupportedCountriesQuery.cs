using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedCountries
{
    public class GetSupportedCountriesQuery : IRequest<IEnumerable<Country>>
    {
        
    }
}