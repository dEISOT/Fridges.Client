using Fridges.Client.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace Fridges.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var response = await _accountService.Login(email, password);
            IEnumerable<string> tokens = response.Headers.GetValues("Set-Cookie");
            string accessToken = tokens.First();
            string refreshToken = tokens.Last();
            Response.Cookies.Append("accessToken", response.Headers.FirstOrDefault(h => h.Key=="accessToken").ToString());
            Response.Cookies.Append("refreshToken", refreshToken);
            return Redirect("/Fridge/Index");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            await _accountService.Register(email, password);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            Response.Cookies.Delete("accessToken");
            return Redirect("/Fridge/Index");
        }
    }
}
