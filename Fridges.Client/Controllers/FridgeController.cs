using Fridges.Client.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers
{
    public class FridgeController : Controller
    {
        private readonly IFridgeService _fridgeService;
        private readonly IAssortmentService _assortmentService;

        public FridgeController(IFridgeService fridgeService, IAssortmentService assortmentService)
        {
            _fridgeService = fridgeService;
            _assortmentService = assortmentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Fridges()
        {

            var fridges = await _fridgeService.GetAsync();
            return View(fridges);
        }
        public async Task<IActionResult> Assortment(Guid fridgeId)
        {
            var products = await _assortmentService.GetProductsAsync(fridgeId);
            ViewBag.Assortments = products;
            return View();
        }
        
    }
}
