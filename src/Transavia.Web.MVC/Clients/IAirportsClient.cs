using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface IAirportsClient
    {
        [Get("airports")]
        Task<Response<IEnumerable<Airport>>> Get(string country, int skipCount, int takeCount);
    }
}