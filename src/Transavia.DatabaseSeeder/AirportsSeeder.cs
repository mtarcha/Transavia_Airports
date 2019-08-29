using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.DatabaseSeeder
{
    public class AirportsSeeder
    {
        private readonly HttpFeedDataProvider _airportsProvider;
        private readonly TransaviaDbContext _ctx;

        public AirportsSeeder(HttpFeedDataProvider airportsProvider, TransaviaDbContext ctx)
        {
            _airportsProvider = airportsProvider;
            _ctx = ctx;
        }

        public async Task SeedAsync(Predicate<AirportEntity> filter, CancellationToken token)
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Airports.Any())
            {
                var airports = await _airportsProvider.GetData(token);
                foreach (var airport in airports.Where(x => filter(x)))
                {
                    await _ctx.Airports.AddAsync(airport, token);
                }

                await _ctx.SaveChangesAsync(token);
            }
        }
    }
}