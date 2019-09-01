using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface ICountriesClient
    {
        [Get("countries")]
        Task<IEnumerable<Country>> GetSupportedCountries();
    }
}