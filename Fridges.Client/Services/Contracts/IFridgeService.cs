using Fridges.Client.Models;

namespace Fridges.Client.Services.Contracts
{
    public interface IFridgeService
    {
        Task<IEnumerable<Fridge>> GetAsync(string jwt);
    }
}
