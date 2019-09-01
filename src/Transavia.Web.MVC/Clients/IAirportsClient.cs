using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface IAirportsClient
    {
        [Get("airports")]
        Task<GetAirportsResult> Get(Guid? country, int skipCount, int takeCount);

        [Get("airports/types")]
        Task<IEnumerable<AirportType>> GetSupportedTypes();

        [Get("airports/statuses")]
        Task<IEnumerable<Status>> GetSupportedStatuses();

        [Get("airports/sizes")]
        Task<IEnumerable<Size>> GetSupportedSizes();

        [Post("airports")]
        Task<Guid> AddAirport([Body] AddAirportModel model);
    }
}