using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Application.Queries.GetSupportedCountries;

namespace Transavia.Application.Queries.Sql
{
    public class GetSupportedAirportCountriesQueryHandler : IGetSupportedAirportCountriesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportCountriesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<IEnumerable<Country>> Handle(GetSupportedCountriesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}