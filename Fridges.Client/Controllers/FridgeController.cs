using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers
{
    public class FridgeController : Controller
    {
        private readonly IFridgeService _fridgeService;
        private readonly IAssortmentService _assortmentService;
        private readonly HttpClient _httpClient;

        public FridgeController(IFridgeService fridgeService, IAssortmentService assortmentService, HttpClient httpClient)
        {
            _fridgeService = fridgeService;
            _assortmentService = assortmentService;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Fridges(int page = 1)
        {
            int pageSize = 3;
            var jwt = HttpContext.Request.Cookies["accessToken"];
            //string token = "Bearer " + jwt;
            //HttpContext.Response.Headers.Authorization.Append(token);
            //Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt).ToString();


            var fridges = await _fridgeService.GetAsync(jwt);
            int count = fridges.Count();
            var items = fridges.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Fridges = items
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Assortment(Guid fridgeId)
        {
            var data = await _assortmentService.GetProductsAsync(fridgeId);
            ViewBag.AssortmentList = data.AssortmentList;
            ViewBag.ProductList = data.ProductList;
            ViewBag.FridgeId = fridgeId;
            return View();
        }

        public async Task<IActionResult> Delete(Guid fridgeId)
        {
            var jwt = HttpContext.Request.Cookies["accessToken"];
            await _fridgeService.DeleteAsync(fridgeId, jwt);
            return Redirect("/Fridge/Fridges");
        }
        private static IDictionary<string, string> ExtractCookiesFromResponse(HttpResponseMessage response)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("Set-Cookie", out values))
            {
                Microsoft.Net.Http.Headers.SetCookieHeaderValue.ParseList(values.ToList()).ToList().ForEach(cookie =>
                {
                    result.Add(cookie.Name.Value, cookie.Value.Value);
                });
            }
            return result;
        }
        
    }
}
