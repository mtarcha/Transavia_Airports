﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Transavia.Web.MVC.Models;

namespace Transavia.Web.MVC.Clients
{
    public interface IStatusesClient
    {
        [Get("statuses")]
        Task<Response<IEnumerable<Status>>> GetSupportedStatuses();
    }
}