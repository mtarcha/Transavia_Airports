using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.Infrastructure.Data
{
    public class EuropeanAirportsSeeder : IStorageSeeder
    {
        private readonly IDataProvider<AirportEntity> _airportsProvider;
        private readonly TransaviaDbContext _ctx;

        public EuropeanAirportsSeeder(IDataProvider<AirportEntity> airportsProvider, TransaviaDbContext ctx)
        {
            _airportsProvider = airportsProvider;
            _ctx = ctx;
        }

        public async Task SeedAsync(CancellationToken token)
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Airports.Any())
            {
                var airports = await _airportsProvider.GetData(token);
                foreach (var airport in airports.Where(x => x.Country.Continent.Code == "EU"))
                {
                    await _ctx.Airports.AddAsync(airport, token);
                }
            }
        }
    }
}