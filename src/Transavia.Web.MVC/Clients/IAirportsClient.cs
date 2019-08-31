using System;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface IAirportsClient
    {
        [Get("airports")]
        Task<Response<GetAirportsResult>> Get(Guid? country, int skipCount, int takeCount);

        [Post("airports")]
        Task<Response<Guid>> AddAirport([Body] AddAirportModel model);
    }
}