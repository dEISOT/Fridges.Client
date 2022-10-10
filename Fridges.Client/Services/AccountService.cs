using Fridges.Client.Models;
using Fridges.Client.Services.Contracts;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Text;

namespace Fridges.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient) => (_httpClient) = (httpClient);
      

        public async Task<HttpResponseMessage> Login(string email, string password)
        {
            Account account = new Account();
            account.Email = email;
            account.Password = password;
            var data = JsonConvert.SerializeObject(account);
            var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            return await _httpClient.PostAsync("/Account", stringContent);
            //response1.Headers.TryGetValues("Set-Cookie", out var values);
            //var accessToken = values.FirstOrDefault(t => t.StartsWith('a'));
        }

        public async Task Logout()
        {
            await _httpClient.GetAsync("/Account/logout");
        }

        public async Task Register(string email, string password)
        {
            Account account = new Account();
            account.Email = email;
            account.Password = password;
            var data = JsonConvert.SerializeObject(account);
            var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var answer = await _httpClient.PostAsync("/Account/Register", stringContent);
        }
    }
}
