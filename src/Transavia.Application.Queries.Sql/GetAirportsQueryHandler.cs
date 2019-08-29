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
                var query = @"select a.Iata, a.Lon, a.Lat, a.Name, st.Name as Status, c.Name as Country, con.Name as Continent, atype.TypeName as Type, si.SizeName as Size
                            from dbo.Airports as a 
	                            FULL OUTER JOIN dbo.Countries as c on a.CountryId = c.Id
	                            FULL OUTER JOIN dbo.Continents as con on c.ContinentId = con.Id
	                            FULL OUTER JOIN dbo.Sizes as si on a.SizeId = si.Id
	                            FULL OUTER JOIN dbo.Statuses as st on a.StatusId = st.Id
	                            FULL OUTER JOIN dbo.AirportTypes as atype on a.TypeId = atype.Id";

                return await connection.QueryAsync<Airport>(query, new {Skip = request.SkipCount});
            }
        }
    }
}