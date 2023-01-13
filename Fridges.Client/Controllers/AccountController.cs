using Fridges.Client.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            var response = await _accountService.Login(email, password);

            HttpContext.Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions { IsEssential = true });
            HttpContext.Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions { IsEssential = true });
            HttpContext.Response.Cookies.Append("Role", response.Role, new CookieOptions { IsEssential = true });  

            return Redirect("/Fridge/Index");
        }
        
        public async Task<IActionResult> Register(string email, string password)
        {
            await _accountService.Register(email, password);
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            await _accountService.Logout(refreshToken, accessToken);
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Delete("Role");
            return Redirect("/Fridge/Index");
        }



    }
}
