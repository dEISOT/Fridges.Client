namespace Fridges.Client.Services.Contracts
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> Login(string email, string password);
        Task Register(string email, string password);
        Task Logout();
    }
}
