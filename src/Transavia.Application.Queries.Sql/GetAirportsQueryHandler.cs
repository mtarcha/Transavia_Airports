using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Transavia.Application.Queries.GetAirports;

namespace Transavia.Application.Queries.Sql
{
    public class GetAirportsQueryHandler : IGetAirportsQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetAirportsQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Airport>> Handle(GetAirportsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.Create())
            {
                var query = @"";

                return await connection.QueryAsync<Airport>(query, new {Skip = request.SkipCount});
            }
        }
    }
}