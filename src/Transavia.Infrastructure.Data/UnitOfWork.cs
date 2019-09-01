using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Repositories;
using Transavia.Infrastructure.EventDispatching;

namespace Transavia.Infrastructure.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly TransaviaDbContext _ctx;
        private readonly IEventDispatcher _eventDispatcher;

        public UnitOfWork(TransaviaDbContext ctx, IEventDispatcher eventDispatcher)
        {
            _ctx = ctx;
            _eventDispatcher = eventDispatcher;
            AirportTypes = new AirportTypesRepository(ctx, eventDispatcher);
            AirportStatuses = new AirportStatusesRepository(ctx, eventDispatcher);
            AirportSizes = new AirportSizesRepository(ctx, eventDispatcher);
            Countries = new CountriesRepository(ctx, eventDispatcher);
            Continents = new ContinentsRepository(ctx, eventDispatcher);
            Airports = new AirportsRepository(ctx, eventDispatcher);
        }

        public IAirportsRepository Airports { get; }
        public IAirportTypesRepository AirportTypes { get; }
        public IAirportStatusesRepository AirportStatuses { get; }
        public IAirportSizesRepository AirportSizes { get; }
        public ICountriesRepository Countries { get; }
        public IContinentsRepository Continents { get; }

        public async Task SaveChanges(CancellationToken token)
        {
            await _ctx.SaveChangesAsync(token);
            _eventDispatcher.RaiseDeferredEvents();
        }
    }
}