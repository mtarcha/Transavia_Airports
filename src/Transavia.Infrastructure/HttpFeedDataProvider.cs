using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Transavia.Infrastructure
{
    public sealed class HttpFeedDataProvider<TData> : IDisposable
    {
        private readonly string _uri;
        private readonly Func<string, IEnumerable<TData>> _deserialize;
        private readonly HttpClient _httpClient;

        public HttpFeedDataProvider(string uri, Func<string, IEnumerable<TData>> deserialize)
        {
            _uri = uri;
            _deserialize = deserialize;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<TData>> GetData(CancellationToken token)
        {
            var response = await _httpClient.GetAsync(_uri, token);
            var jsonString = await response.Content.ReadAsStringAsync();

            return _deserialize(jsonString);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}