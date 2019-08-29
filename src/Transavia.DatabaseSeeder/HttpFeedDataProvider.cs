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

        private Dictionary<string, string> _continentCodeToNameMap = new Dictionary<string, string>
        {
            { "AF", "Africa" },
            { "AN", "Antarctica" },
            { "AS", "Asia" },
            { "EU", "Europe" },
            { "OC", "Oceania" },
            { "NA", "North america" },
            { "SA", "South america" },
        };

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
                    continent = new ContinentEntity
                    {
                        Code = airport.continent,
                        Name = _continentCodeToNameMap[airport.continent]
                    };
                    continents.Add(airport.continent, continent);
                }

                if (!countries.TryGetValue(airport.iso, out var country))
                {
                    country = new CountryEntity
                    {
                        Iso = airport.iso,
                        Name = GetCountryName(airport.iso),
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
                    status = new StatusEntity
                    {
                        Code = airport.status,
                        Name = airport.status == 0 ? "inactive" : "active"
                    };
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
                    Lat = airport.lat,
                    Status = status
                });
            }

            return airports;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        // todo: to dictionary
        private string GetCountryName(string iso)
        {
            switch (iso)
            {
                case "AF": return "Afghanistan";
                case "AX": return "Aland Islands";
                case "AL": return "Albania";
                case "DZ": return "Algeria";
                case "AS": return "American Samoa";
                case "AD": return "Andorra";
                case "AO": return "Angola";
                case "AI": return "Anguilla";
                case "AQ": return "Antarctica";
                case "AB": return "Antigua and Barbuda";
                case "AG": return "Argentina";
                case "AM": return "Armenia";
                case "AW": return "Aruba";
                case "AU": return "Australia ";
                case "AT": return "Austria";
                case "AZ": return "Azerbaijan";
                case "BS": return "Bahamas";
                case "BH": return "Bahrain";
                case "BD": return "Bangladesh";
                case "BB": return "Barbados";
                case "BY": return "Belarus";
                case "BE": return "Belgium";
                case "BZ": return "Belize";
                case "BJ": return "Benin";
                case "BM": return "Bermuda";
                case "BT": return "Bhutan";
                case "BO": return "Bolivia";
                case "BA": return "Bosnia and Herzegovina";
                case "BW": return "Botswana";
                case "BV": return "Bouvet Island";
                case "BR": return "Brazil";
                case "VG": return "British Virgin Islands";
                case "IO": return "British Indian Ocean Territory";
                case "BN": return "Brunei Darussalam";
                case "BG": return "Bulgaria";
                case "BF": return "Burkina Faso";
                case "BI": return "Burundi";
                case "KH": return "Cambodia";
                case "CM": return "Cameroon";
                case "CA": return "Canada";
                case "CV": return "Cape Verde";
                case "KY": return "Cayman Islands";
                case "CF": return "Central African Republic";
                case "TD": return "Chad";
                case "CL": return "Chile";
                case "CN": return "China";
                case "HK": return "Hong Kong";
                case "MO": return "Macao";
                case "CX": return "Christmas Island";
                case "CC": return "Cocos";
                case "CO": return "Colombia";
                case "KM": return "Comoros";
                case "CG": return "Congo (Brazzaville)";
                case "CD": return "Congo: return (Kinshasa)";
                case "CK": return "Cook Islands";
                case "CR": return "Costa Rica";
                case "CI": return "Cote d'Ivoire";
                case "HR": return "Croatia";
                case "CU": return "Cuba";
                case "CY": return "Cyprus";
                case "CZ": return "Czech Republic";
                case "DK": return "Denmark";
                case "DJ": return "Djibouti";
                case "DM": return "Dominica";
                case "DO": return "Dominican Republic";
                case "EC": return "Ecuador";
                case "EG": return "Egypt";
                case "SV": return "El Salvador";
                case "GQ": return "Equatorial Guinea";
                case "ER": return "Eritrea";
                case "EE": return "Estonia";
                case "ET": return "Ethiopia";
                case "FK": return "Falkland Islands";
                case "FO": return "FaroeIslands";
                case "FJ": return "Fiji";
                case "FI": return "Finland";
                case "FR": return "France";
                case "GF": return "French Guiana";
                case "PF": return "French Polynesia";
                case "TF": return "French Southern Territories";
                case "GB": return "Gabon";
                case "GM": return "Gambia";
                case "GE": return "Georgia";
                case "DE": return "Germany";
                case "GH": return "Ghana";
                case "GI": return "Gibraltar";
                case "GR": return "Greece";
                case "GL": return "Greenland";
                case "GD": return "Grenada";
                case "GP": return "Guadeloupe";
                case "GU": return "Guam";
                case "GT": return "Guatemala";
                case "GG": return "Guernsey";
                case "GN": return "Guinea";
                case "GW": return "Guinea-Bissau";
                case "GY": return "Guyana";
                case "HT": return "Haiti";
                case "HM": return "Heard and Mcdonald Islands";
                case "VA": return "Holy See";
                case "HN": return "Honduras";
                case "HU": return "Hungary";
                case "IS": return "Iceland";
                case "IN": return "India";
                case "ID": return "Indonesia";
                case "IR": return "Iran";
                case "IQ": return "Iraq";
                case "IE": return "Ireland";
                case "IM": return "Isle of Man";
                case "IL": return "Israel";
                case "IT": return "Italy";
                case "JM": return "	Jamaica";
                case "JP": return "	Japan";
                case "JE": return "Jersey";
                case "JO": return "Jordan";
                case "KZ": return "	Kazakhstan";
                case "KE": return "Kenya";
                case "KI": return "Kiribati";
                case "KP": return "Korea (North)";
                case "KR": return "Korea (South)";
                case "KW": return "	Kuwait";
                case "KG": return "	Kyrgyzstan";
                case "LA": return "Lao PDR";
                case "LV": return "Latvia";
                case "LB": return "Lebanon";
                case "LS": return "	Lesotho";
                case "LR": return "Liberia";
                case "LY": return "Libya";
                case "LI": return "Liechtenstein";
                case "LT": return "Lithuania";
                case "LU": return "Luxembourg";
                case "MK": return "	Macedonia";
                case "MG": return "	Madagascar";
                case "MW": return "Malawi";
                case "MY": return "Malaysia";
                case "MV": return "Maldives";
                case "ML": return "Mali";
                case "MT": return "Malta";
                case "MH": return "	Marshall Islands";
                case "MQ": return "	Martinique";
                case "MR": return "Mauritania";
                case "MU": return "Mauritius";
                case "MX": return "Mexico";
                case "FM": return "	Micronesia";
                case "MD": return "	Moldova";
                case "MC": return "	Monaco";
                case "MN": return "Mongolia";
                case "ME": return "Montenegro";
                case "MS": return "Montserrat";
                case "MA": return "Morocco";
                case "MZ": return "Mozambique";
                case "MM": return "	Myanmar";
                case "NA": return "Namibia";
                case "NR": return "	Nauru";
                case "NP": return "Nepal";
                case "NL": return "Netherlands";
                case "AN": return "Netherlands Antilles";
                case "NC": return "	New Caledonia";
                case "NZ": return "	New Zealand";
                case "NI": return "Nicaragua";
                case "NE": return "Niger";
                case "NG": return "	Nigeria";
                case "NU": return "	Niue";
                case "NF": return "Norfolk Island";
                case "MP": return "	Northern Mariana Islands";
                case "NO": return "Norway";
                case "OM": return "Oman";
                default: return iso;
            }
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