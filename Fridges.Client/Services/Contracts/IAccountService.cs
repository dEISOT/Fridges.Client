using Fridges.Client.Models;

namespace Fridges.Client.Services.Contracts
{
    public interface IAccountService
    {
        Task<AuthResponseModel> Login(string email, string password);
        Task Register(string email, string password);
        Task Logout(string refreshToken, string accessToken);
    }
}
