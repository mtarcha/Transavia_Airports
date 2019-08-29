using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Repositories;

namespace Transavia.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransaviaDbContext _ctx;

        public UnitOfWork(TransaviaDbContext ctx)
        {
            _ctx = ctx;
            AirportTypes = new AirportTypesRepository(ctx);
            Statuses = new StatusesRepository(ctx);
            Sizes = new SizesRepository(ctx);
            Countries = new CountriesRepository(ctx);
            Continents = new ContinentsRepository(ctx);
            Airports = new AirportsRepository(ctx);
        }

        public IAirportsRepository Airports { get; }
        public IAirportTypesRepository AirportTypes { get; }
        public IStatusesRepository Statuses { get; }
        public ISizesRepository Sizes { get; }
        public ICountriesRepository Countries { get; }
        public IContinentsRepository Continents { get; }

        public async Task SaveChanges(CancellationToken token)
        {
            await _ctx.SaveChangesAsync(token);
        }
    }
}