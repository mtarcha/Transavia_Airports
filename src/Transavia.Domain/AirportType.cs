using Transavia.Domain.Common;

namespace Transavia.Domain
{
    public class AirportType : Enumeration<AirportType, int>
    {
        public static AirportType Airport = new AirportType(1, "airport");
        public static AirportType Heliport = new AirportType(2, "heliport");
        public static AirportType Seaplanes = new AirportType(3, "seaplanes");
        public static AirportType Closed = new AirportType(4, "closed");
       
        private AirportType(int id, string name) : base(id, name)
        {
        }
    }
}