using System;
using Transavia.Domain.Common;

namespace Transavia.Domain
{
    public class Airport : Entity<Guid>, IAggregateRoot
    {
        public Airport(
            IEventDispatcher eventDispatcher,
            string iata,
            CountryCode countryCode,
            string name,
            AirportType type,
            Continent continent,
            Size size,
            int status,
            string lon,
            string lat)
            : this(Guid.NewGuid(), eventDispatcher, iata, countryCode, name, type, continent, size, status, lon, lat)
        {
            RaiseEventDeferred(new NewAirportEvent(Id));
        }

        public Airport(
            Guid id, 
            IEventDispatcher eventDispatcher, 
            string iata, 
            CountryCode countryCode, 
            string name, 
            AirportType type, 
            Continent continent, 
            Size size, 
            int status, 
            string lon, 
            string lat) 
            : base(id, eventDispatcher)
        {
            if (countryCode == null)
                throw new ArgumentNullException(nameof(countryCode));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (continent == null)
                throw new ArgumentNullException(nameof(continent));
            if (size == null)
                throw new ArgumentNullException(nameof(size));
            if (string.IsNullOrEmpty(iata))
                throw new ArgumentException("Value cannot be null or empty.", nameof(iata));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (string.IsNullOrEmpty(lon))
                throw new ArgumentException("Value cannot be null or empty.", nameof(lon));
            if (string.IsNullOrEmpty(lat))
                throw new ArgumentException("Value cannot be null or empty.", nameof(lat));
            
            Iata = iata;
            CountryCode = countryCode;
            Name = name;
            Type = type;
            Continent = continent;
            Size = size;
            Status = status; // todo: investigate valid values
            Lon = lon;
            Lat = lat;
        }
        
        // todo: investigate what values can be here and what does it mean. Probably it is enumeration
        private string Iata { get; }

        public CountryCode CountryCode { get; }

        public string Name { get; }

        public AirportType Type { get; }

        public Continent Continent { get; }

        public Size Size { get; }

        // todo: investigate what values can be here and what does it mean. Probably it is enumeration
        private int Status { get; }

        // todo: investigate what values can be here and what does it mean.
        public string Lon { get; }

        // todo: investigate what values can be here and what does it mean. 
        public string Lat { get; }
    }
}