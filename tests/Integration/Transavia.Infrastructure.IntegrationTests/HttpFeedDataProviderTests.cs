using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Transavia.Infrastructure.Data;

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

        [TearDown]
        public void Teardown()
        {
            _httpFeedDataProvider.Dispose();
        }

        [Test]
        public async Task GetData_AllAirportsDataRetrieved()
        {
            var airports =  await _httpFeedDataProvider.GetData(CancellationToken.None);
            
            Assert.That(airports.Count() == 6726);
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