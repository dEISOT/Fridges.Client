using Fridges.Client.Models;

namespace Fridges.Client.Services.Contracts
{
    public interface IHttpFridgeClient
    {
        Task<AssortmentWithProduct> GetAsync(Guid fridgeId);
    }
}
