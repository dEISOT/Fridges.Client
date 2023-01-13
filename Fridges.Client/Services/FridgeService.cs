using Fridges.Client.Services.Contracts;
using Fridges.Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Fridges.Client.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly HttpClient _httpClient;

        public FridgeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Add(string name, string typeId, string jwt)
        {
            FridgeType model = new FridgeType()
            {
                Name = name,
                TypeId = new Guid(typeId)
            };
            var data = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add( 
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            await _httpClient.PostAsync("/Fridge", stringContent);
        }

        public async Task DeleteAsync(Guid fridgeId, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            await _httpClient.DeleteAsync($"/Fridge/{fridgeId}");
        }

        public async Task<FridgeViewModel> GetAsync(string jwt)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            using var response = await _httpClient.GetAsync("/Fridge");
            return JsonConvert.DeserializeObject<FridgeViewModel>(await response.Content.ReadAsStringAsync());

        }
    }
}
