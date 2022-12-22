using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Fridges.Client.Services
{
    public class HttpFridgeClient : IHttpFridgeClient
    {
        private readonly HttpClient _httpClient;


        public HttpFridgeClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7183");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient = httpClient;
        }

        public async Task<AssortmentWithProduct> GetAsync(Guid fridgeId)
        {
            var assortment = await _httpClient.GetAsync($"/Assortment/{fridgeId}");
            string content = await assortment.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AssortmentWithProduct>(content);
        }
    }
}
