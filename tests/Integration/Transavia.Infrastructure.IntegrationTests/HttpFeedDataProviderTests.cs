using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Transavia.Infrastructure.IntegrationTests
{
    [TestFixture]
    public class HttpFeedDataProviderTests
    {
        private HttpFeedDataProvider<AirportData> _httpFeedDataProvider;

        [SetUp]
        public void Setup()
        {
            _httpFeedDataProvider = new HttpFeedDataProvider<AirportData>(
                "https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json",
                JsonConvert.DeserializeObject<List<AirportData>>);
        }

        [Test]
        public void GetData_CanRetrieveMultipleAirports()
        {
            var airports = _httpFeedDataProvider.GetData(CancellationToken.None).Result.ToList();

            Assert.That(airports.Count > 1);
        }

        [TearDown]
        public void Teardown()
        {
            _httpFeedDataProvider.Dispose();
        }

        [Test]
        public void GetData_CanRetrieveAirportsFromDifferentCountries()
        {
            var airports = _httpFeedDataProvider.GetData(CancellationToken.None).Result.ToList();

            var countries = airports.Select(x => x.iso).Distinct();

            Assert.That(countries.Count() > 1);
        }

        [Test]
        public void GetData_CanRetrieveAirportsFromDifferentContinents()
        {
            var airports = _httpFeedDataProvider.GetData(CancellationToken.None).Result.ToList();

            var continents = airports.Select(x => x.continent).Distinct();

            Assert.That(continents.Count() > 1);
        }

        private class AirportData
        {
            public string iata { get; set; }
            public string lon { get; set; }
            public string iso { get; set; }
            public int status { get; set; }
            public string name { get; set; }
            public string continent { get; set; }
            public string type { get; set; }
            public string lat { get; set; }
            public string size { get; set; }
        }
    }
}