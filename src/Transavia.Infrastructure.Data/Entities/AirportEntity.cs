﻿namespace Transavia.Infrastructure.Data.Entities
{
    public class AirportEntity : Entity
    {
        public string Iata { get; set; }

        public string Name { get; set; }

        public CountryEntity Country { get; set; }

        public AirportTypeEntity Type { get; set; }
        
        public SizeEntity Size { get; set; }

        public StatusEntity Status { get; set; }

        // todo: investigate what values can be here and what does it mean.
        public string Lon { get; set; }

        // todo: investigate what values can be here and what does it mean. 
        public string Lat { get; set; }
    }
}