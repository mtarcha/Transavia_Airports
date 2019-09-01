using System;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.Infrastructure.Data.Repositories
{
    public interface IRepository<TEntity, in TId>
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken token);

        Task<TEntity> GetByIdAsync(TId id, CancellationToken token);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token);

        Task DeleteAsync(TId id, CancellationToken token);
    }

    public interface IAirportsRepository : IRepository<AirportEntity, Guid>
    {
    }

    public interface ICountriesRepository : IRepository<CountryEntity, Guid>
    {
    }

    public interface IContinentsRepository : IRepository<ContinentEntity, Guid>
    {
    }

    public interface IAirportSizesRepository : IRepository<SizeEntity, Guid>
    {
    }

    public interface IAirportStatusesRepository : IRepository<StatusEntity, Guid>
    {
    }

    public interface IAirportTypesRepository : IRepository<AirportTypeEntity, Guid>
    {
    }
}