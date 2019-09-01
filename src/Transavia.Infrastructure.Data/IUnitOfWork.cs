using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Repositories;

namespace Transavia.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IAirportsRepository Airports { get; }

        IAirportTypesRepository AirportTypes { get; }

        IAirportStatusesRepository AirportStatuses { get; }

        IAirportSizesRepository AirportSizes { get; }

        ICountriesRepository Countries { get; }

        IContinentsRepository Continents { get; }
        
        Task SaveChanges(CancellationToken token);
    }
}