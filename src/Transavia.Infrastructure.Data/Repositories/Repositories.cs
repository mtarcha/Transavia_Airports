using System;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.Infrastructure.Data.Repositories
{
    public sealed class AirportsRepository : RepositoryBase<AirportEntity>, IAirportsRepository
    {
        public AirportsRepository(TransaviaDbContext ctx) : base(ctx)
        {
        }
    }

    public sealed class CountriesRepository : RepositoryBase<CountryEntity>, ICountriesRepository
    {
        public CountriesRepository(TransaviaDbContext ctx) : base(ctx)
        {
        }
    }

    public sealed class ContinentsRepository : RepositoryBase<ContinentEntity>, IContinentsRepository
    {
        public ContinentsRepository(TransaviaDbContext ctx) : base(ctx)
        {
        }
    }

    public sealed class SizesRepository : RepositoryBase<SizeEntity>, ISizesRepository
    {
        public SizesRepository(TransaviaDbContext ctx) : base(ctx)
        {
        }
    }

    public sealed class StatusesRepository : RepositoryBase<StatusEntity>, IStatusesRepository
    {
        public StatusesRepository(TransaviaDbContext ctx) : base(ctx)
        {
        }
    }

    public sealed class AirportTypesRepository : RepositoryBase<AirportTypeEntity>, IAirportTypesRepository
    {
        public AirportTypesRepository(TransaviaDbContext ctx) : base(ctx)
        {
        }
    }
}