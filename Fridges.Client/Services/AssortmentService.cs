using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace Fridges.Client.Services
{
    public class Assortmentservice : IAssortmentService
    {
        private readonly IHttpFridgeClient _httpClient;

        public Assortmentservice(IHttpFridgeClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AssortmentWithProduct> GetProductsAsync(Guid fridgeId)
        {
            var assortment = await _httpClient.GetAsync(fridgeId);
            //string test1 = await assortment.Content.ReadAsStringAsync();
            //var test = JsonConvert.DeserializeObject<AssortmentWithProduct>(test1);
            return assortment;

        } 
        //public async Task DeleteAsync(string assortmentId)
        //{
        //    await _httpClient.DeleteAsync($"/Assortment/{assortmentId}");
        //}

        //public async Task DeleteAllAsync(string fridgeId)
        //{
        //    await _httpClient.DeleteAsync($"/Assortment/{fridgeId}/deleteAll");
        //}

        //public async Task AddAsync(string fridgeId, string productId, int quantity)
        //{
        //    AssortmentPost model = new AssortmentPost 
        //    {
        //        FridgeId = new Guid(fridgeId),
        //        ProductId = new Guid(productId),
        //        Quantity = quantity
        //    };
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(model), UnicodeEncoding.UTF8, "application/json");

        //    await _httpClient.PostAsync("/Assortment", stringContent);
        //}

        //public async Task UpdateAsync(string assortmentId, int newQuantity)
        //{
        //    AssortmentUpdate model = new AssortmentUpdate
        //    {
        //        AssortmentId = new Guid(assortmentId),
        //        NewQuantity = newQuantity
        //    };
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(model), UnicodeEncoding.UTF8, "application/json");
        //    await _httpClient.PutAsync("/Assortment", stringContent);

        //}
    }
}
