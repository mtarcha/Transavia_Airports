﻿using System;
using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetAirports
{
    public class GetAirportsQuery : IRequest<IEnumerable<Airport>>
    {
        public Guid? CountryId { get; set; }

        public int SkipCount { get; set; }

        public int TakeCount { get; set; }
    }
}