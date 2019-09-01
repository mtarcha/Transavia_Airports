using Transavia.Infrastructure.Data.Entities;
using Transavia.Infrastructure.EventDispatching;

namespace Transavia.Infrastructure.Data.Repositories
{
    public sealed class AirportsRepository : RepositoryBase<AirportEntity>, IAirportsRepository
    {
        public AirportsRepository(TransaviaDbContext ctx, IEventDispatcher eventDispatcher) : base(ctx, eventDispatcher)
        {
        }
    }

    public sealed class CountriesRepository : RepositoryBase<CountryEntity>, ICountriesRepository
    {
        public CountriesRepository(TransaviaDbContext ctx, IEventDispatcher eventDispatcher) : base(ctx, eventDispatcher)
        {
        }
    }

    public sealed class ContinentsRepository : RepositoryBase<ContinentEntity>, IContinentsRepository
    {
        public ContinentsRepository(TransaviaDbContext ctx, IEventDispatcher eventDispatcher) : base(ctx, eventDispatcher)
        {
        }
    }

    public sealed class AirportSizesRepository : RepositoryBase<SizeEntity>, IAirportSizesRepository
    {
        public AirportSizesRepository(TransaviaDbContext ctx, IEventDispatcher eventDispatcher) : base(ctx, eventDispatcher)
        {
        }
    }

    public sealed class AirportStatusesRepository : RepositoryBase<StatusEntity>, IAirportStatusesRepository
    {
        public AirportStatusesRepository(TransaviaDbContext ctx, IEventDispatcher eventDispatcher) : base(ctx, eventDispatcher)
        {
        }
    }

    public sealed class AirportTypesRepository : RepositoryBase<AirportTypeEntity>, IAirportTypesRepository
    {
        public AirportTypesRepository(TransaviaDbContext ctx, IEventDispatcher eventDispatcher) : base(ctx, eventDispatcher)
        {
        }
    }
}