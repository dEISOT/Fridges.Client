using Fridges.Client.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers
{
    public class AssortmentController : Controller
    {
        private readonly IAssortmentService _assortmentService;
        private readonly HttpClient _httpClient;

        public AssortmentController(IAssortmentService assortmentService, HttpClient httpClient)
        {
            _assortmentService = assortmentService;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Delete(string assortmentId)
        {
            await _assortmentService.DeleteAsync(assortmentId);
            return Redirect("/Fridge/Fridges");
        }

        public async Task<IActionResult> DeleteAll(string fridgeId)
        {
            await _assortmentService.DeleteAllAsync(fridgeId);
            return Redirect("/Fridge/Fridges");
        }

        [HttpPost]
        public async Task<IActionResult> Put(string assortmentId, int newQuantity)
        {
            await _assortmentService.UpdateAsync(assortmentId, newQuantity);
            return Redirect("/Fridge/Fridges");
        }
        [HttpPost]
        public async Task<IActionResult> Add(string fridgeId, string productId, int quantity)
        {
            await _assortmentService.AddAsync(fridgeId, productId, quantity);
            return Redirect($"/Fridge/Assortment?fridgeId={fridgeId}");
        }
    }
}
