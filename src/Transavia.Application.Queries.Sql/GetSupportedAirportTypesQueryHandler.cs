using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Transavia.Application.Queries.GetSupportedAirportTypes;

namespace Transavia.Application.Queries.Sql
{
    public sealed class GetSupportedAirportTypesQueryHandler : IGetSupportedAirportTypesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportTypesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<AirportType>> Handle(GetSupportedAirportTypesQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.Create())
            {
                return await connection.QueryAsync<AirportType>("select Id, TypeName as Name from dbo.AirportTypes");
            }
        }
    }
}