using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Transavia.Application.Queries.GetSupportedCountries;

namespace Transavia.Application.Queries.Sql
{
    public sealed class GetSupportedAirportCountriesQueryHandler : IGetSupportedAirportCountriesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportCountriesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Country>> Handle(GetSupportedCountriesQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.Create())
            {
                return await connection.QueryAsync<Country>("select Id, Name from dbo.Countries");
            }
        }
    }
}