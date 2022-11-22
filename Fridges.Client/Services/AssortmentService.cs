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
            var assortment = await _httpClient.GetAsync($"/Assortment/{fridgeId}");
            return JsonConvert.DeserializeObject<IEnumerable<Assortment>>(await assortment.Content.ReadAsStringAsync());

        } 
        public async Task DeleteAsync(string assortmentId)
        {
            await _httpClient.DeleteAsync($"/Assortment/{assortmentId}");
        }

        public async Task DeleteAllAsync(string fridgeId)
        {
            await _httpClient.DeleteAsync($"/Assortment/{fridgeId}/deleteAll");
        }
    }
}
