using CommonLib;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class PetService : IHttpClient
    {
        private HttpClient _client;
        private string _uri;

        public PetService(string uri)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _uri = uri;
        }

        public async Task<string> GetResultsAsync()
        {
            using (_client)
            {
                HttpResponseMessage response = await _client.GetAsync(_uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                // Error occurs
                return $"Oh no, something went wrong: Status Code - {(int)response.StatusCode}";
            }
        }
    }
}
