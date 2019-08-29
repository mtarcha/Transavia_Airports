using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public Task<IEnumerable<Status>> Handle(GetSupportedAirportStatusesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}