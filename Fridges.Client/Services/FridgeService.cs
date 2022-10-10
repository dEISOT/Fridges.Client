using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Newtonsoft.Json;

namespace Fridges.Client.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly HttpClient _httpClient;

        public FridgeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Fridge>> GetAsync()
        {
            var fridges = await _httpClient.GetAsync("/Fridge");

            return JsonConvert.DeserializeObject<IEnumerable<Fridge>>(await fridges.Content.ReadAsStringAsync());
        }
    }
}
