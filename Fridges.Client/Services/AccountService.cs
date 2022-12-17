using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Fridges.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient) => (_httpClient) = (httpClient);
      

        public async Task<AuthResponseModel> Login(string email, string password)
        {
            Account account = new Account();
            account.Email = email;
            account.Password = password;
            var data = JsonConvert.SerializeObject(account);
            var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Account", stringContent);
            var result = JsonConvert.DeserializeObject<AuthResponseModel>(await response.Content.ReadAsStringAsync());
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(result.AccessToken);
            string role = jwt.Claims.FirstOrDefault(c => c.Type == "role").Value;
            result.Role = role;
            return result;
            //response1.Headers.TryGetValues("Set-Cookie", out var values);
            //var accessToken = values.FirstOrDefault(t => t.StartsWith('a'));
        }

        public async Task Logout(string refreshToken, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            await _httpClient.GetAsync($"/Account/logout/{refreshToken}");
        }

        public async Task Register(string email, string password)
        {
            Account account = new Account
            {
                Email = email, 
                Password = password
            };
            var data = JsonConvert.SerializeObject(account);
            var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");  
            var answer = await _httpClient.PostAsync("/Account/Register", stringContent);
        }
    }
}
