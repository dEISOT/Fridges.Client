using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Fridges.Client.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly HttpClient _httpClient;

        public FridgeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Fridge>> GetAsync(string jwt)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            //using var client = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            try
            {
                using var response = await _httpClient.GetAsync("/Fridge");
                return JsonConvert.DeserializeObject<IEnumerable<Fridge>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
