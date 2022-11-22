using Fridges.Client.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
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
            //var response = await _accountService.Login(email, password);
            //IEnumerable<string> tokens = response.Headers.GetValues("Set-Cookie");
            //string accessToken = tokens.First();
            //string refreshToken = tokens.Last();
            //Response.Cookies.Append("accessToken", response.Headers.FirstOrDefault(h => h.Key=="accessToken").ToString());
            //Response.Cookies.Append("refreshToken", refreshToken);
            //return Redirect("/Fridge/Index");


            var response = await _accountService.Login(email, password);
            
            var Cookies = ExtractCookiesFromResponse(response);

            foreach (var item in Cookies)
            {
                HttpContext.Response.Cookies.Append(
                    item.Key,
                    item.Value,
                    new CookieOptions { IsEssential = true });
            }
            
            var check = User.Identity.IsAuthenticated;
            

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

        private IDictionary<string, string> ExtractCookiesFromResponse()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            StringValues values;
            var check = HttpContext.Response.Headers.TryGetValue("Set-Cookie", out values);
            if (check)
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
