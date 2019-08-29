using System.Linq;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using Transavia.DatabaseSeeder;

namespace Transavia.Infrastructure.Data.IntegrationTests
{
    [TestFixture]
    public class HttpFeedDataProviderTests
    {
        private HttpFeedDataProvider _httpFeedDataProvider;

        [SetUp]
        public void Setup()
        {
            _httpFeedDataProvider = new HttpFeedDataProvider("https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json");
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

            var countries = airports.Select(x => x.Country.Iso).Distinct();

            Assert.That(countries.Count() > 1);
        }

        [Test]
        public void GetData_CanRetrieveAirportsFromDifferentContinents()
        {
            var airports = _httpFeedDataProvider.GetData(CancellationToken.None).Result.ToList();

            var continents = airports.Select(x => x.Country.Continent.Code).Distinct();

            Assert.That(continents.Count() > 1);
        }
    }
}