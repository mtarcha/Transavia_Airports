using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Repositories;
using Transavia.Infrastructure.EventDispatching;

namespace Transavia.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransaviaDbContext _ctx;
        private readonly IEventDispatcher _eventDispatcher;

        public UnitOfWork(TransaviaDbContext ctx, IEventDispatcher eventDispatcher)
        {
            _ctx = ctx;
            _eventDispatcher = eventDispatcher;
            AirportTypes = new AirportTypesRepository(ctx, eventDispatcher);
            Statuses = new StatusesRepository(ctx, eventDispatcher);
            Sizes = new SizesRepository(ctx, eventDispatcher);
            Countries = new CountriesRepository(ctx, eventDispatcher);
            Continents = new ContinentsRepository(ctx, eventDispatcher);
            Airports = new AirportsRepository(ctx, eventDispatcher);
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
            _eventDispatcher.RaiseDeferredEvents();
        }
    }
}