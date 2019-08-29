using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.DatabaseSeeder
{
    public sealed class HttpFeedDataProvider : IDisposable
    {
        private readonly string _uri;
        private readonly HttpClient _httpClient;

        public HttpFeedDataProvider(string uri)
        {
            _uri = uri;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<AirportEntity>> GetData(CancellationToken token)
        {
            var response = await _httpClient.GetAsync(_uri, token);
            var jsonString = await response.Content.ReadAsStringAsync();
            var internalAirports = JsonConvert.DeserializeObject<List<AirportInternal>>(jsonString);

            var airports = new List<AirportEntity>();
            var countries = new Dictionary<string, CountryEntity>();
            var continents = new Dictionary<string, ContinentEntity>();
            var statuses = new Dictionary<int, StatusEntity>();
            var sizes = new Dictionary<string, SizeEntity>();
            var airportTypes = new Dictionary<string, AirportTypeEntity>();
            foreach (var airport in internalAirports)
            {
                if (!continents.TryGetValue(airport.continent, out var continent))
                {
                    continent = new ContinentEntity { Code = airport.continent };
                    continents.Add(airport.continent, continent);
                }

                if (!countries.TryGetValue(airport.iso, out var country))
                {
                    country = new CountryEntity
                    {
                        Iso = airport.iso,
                        Continent = continent
                    };
                    countries.Add(airport.iso, country);
                }

                SizeEntity size = null;
                if (!string.IsNullOrEmpty(airport.size) && !sizes.TryGetValue(airport.size, out size))
                {
                    size = new SizeEntity { SizeName = airport.size };
                    sizes.Add(airport.size, size);
                }

                if (!airportTypes.TryGetValue(airport.type, out var airportType) && airport.type != "closed")
                {
                    airportType = new AirportTypeEntity { TypeName = airport.type };
                    airportTypes.Add(airport.type, airportType);
                }

                if (!statuses.TryGetValue(airport.status, out var status))
                {
                    status = new StatusEntity { Code = airport.status };
                    statuses.Add(airport.status, status);
                }

                airports.Add(new AirportEntity
                {
                    Iata = airport.iata,
                    Name = airport.name,
                    Country = country,
                    Type = airportType,
                    Size = size,
                    Lon = airport.lon,
                    Lat = airport.lat
                });
            }

            return airports;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private class AirportInternal
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