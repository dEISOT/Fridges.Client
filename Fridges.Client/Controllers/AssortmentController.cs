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
    }
}
