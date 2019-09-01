using System;
using System.Net.Http;
using NUnit.Framework;
using Transavia.API.PerformanceTests.Benchmarking;

namespace Transavia.API.PerformanceTests
{
    [TestFixture]
    public class AirportsApiPerformanceTests
    {
        private HttpClient _apiClient;
       
        [SetUp]
        public void Setup()
        {
            _apiClient = new HttpClient();
        }

        [Test]
        public void GetAirportsTest()
        {
            var statistics = BenchmarkRunner.Run(
                () => _apiClient.GetAsync("http://localhost:7777/api/airports?skipCount=0&takeCount=30").Wait(), 
                10, 
                4);

            statistics.EvaluateStatistics();

            Assert.That(statistics.AverageExecutionTime, Is.LessThanOrEqualTo(TimeSpan.FromMilliseconds(3)));
            Assert.That(statistics.RunsPerSecond, Is.GreaterThanOrEqualTo(1500));
        }
    }
}