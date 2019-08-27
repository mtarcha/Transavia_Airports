using Transavia.Domain.Common;

namespace Transavia.Domain
{
    public class Continent : Enumeration<Continent, string>
    {
        public static Continent Africa = new Continent("AF", "Africa");
        public static Continent Antarctica = new Continent("AN", "Antarctica");
        public static Continent Asia = new Continent("EAS", "Asia");
        public static Continent Europe = new Continent("EU", "Europe");
        public static Continent Oceania = new Continent("OC", "Oceania");
        public static Continent NorthAmerica = new Continent("NA", "North america");
        public static Continent SouthAmerica = new Continent("SA", "South america");

        private Continent(string id, string name) : base(id, name)
        {
        }
    }
}