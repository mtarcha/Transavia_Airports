using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface ITypesClient
    {
        [Get("types")]
        Task<Response<IEnumerable<AirportType>>> GetSupportedTypes();
    }
}