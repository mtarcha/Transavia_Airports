using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Transavia.Application.Queries.GetSupportedAirportStatuses;

namespace Transavia.Application.Queries.Sql
{
    public class GetSupportedAirportStatusesQueryHandler : IGetSupportedAirportStatusesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportStatusesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Status>> Handle(GetSupportedAirportStatusesQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.Create())
            {
                return await connection.QueryAsync<Status>("select Id, Name from dbo.Statuses");
            }
        }
    }
}