using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Newtonsoft.Json;

namespace Fridges.Client.Services
{
    public class Assortmentservice : IAssortmentService
    {
        private readonly HttpClient _httpClient;

        public Assortmentservice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Assortment>> GetProductsAsync(Guid fridgeId)
        {
            var assortment = await _httpClient.GetAsync($"/api/Assortment/{fridgeId}");
            return JsonConvert.DeserializeObject<IEnumerable<Assortment>>(await assortment.Content.ReadAsStringAsync());

        }
    }
}
