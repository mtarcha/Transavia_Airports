using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface ISizesClient
    {
        [Get("sizes")]
        Task<Response<IEnumerable<Size>>> GetSupportedSizes();
    }
}