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
            //var response = await _accountService.Login(email, password);
            //IEnumerable<string> tokens = response.Headers.GetValues("Set-Cookie");
            //string accessToken = tokens.First();
            //string refreshToken = tokens.Last();
            //Response.Cookies.Append("accessToken", response.Headers.FirstOrDefault(h => h.Key=="accessToken").ToString());
            //Response.Cookies.Append("refreshToken", refreshToken);
            //return Redirect("/Fridge/Index");


            var response = await _accountService.Login(email, password);

            //var Cookies = ExtractCookiesFromResponse(response);
            //HttpContext.Response.Cookies.Append(
            //        "a",
            //        item.Value,
            //        new CookieOptions { IsEssential = true });
            //var jwt = new JwtSecurityTokenHandler().ReadJwtToken(response.AccessToken);
            //string user = jwt.Claims.First(c => c.Type == "user").Value;

            HttpContext.Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions { IsEssential = true });
            HttpContext.Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions {  IsEssential = true });
            HttpContext.Response.Cookies.Append("role", response.Role, new CookieOptions { IsEssential = true });  
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


    }
}
