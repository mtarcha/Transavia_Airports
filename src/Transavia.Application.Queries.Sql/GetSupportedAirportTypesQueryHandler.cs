using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Application.Queries.GetSupportedAirportTypes;

namespace Transavia.Application.Queries.Sql
{
    public class GetSupportedAirportTypesQueryHandler : IGetSupportedAirportTypesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportTypesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<IEnumerable<AirportType>> Handle(GetSupportedAirportTypesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}