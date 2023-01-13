using Fridges.Client.Models;
using Fridges.Client.ViewModels;

namespace Fridges.Client.Services.Contracts
{
    public interface IFridgeService
    {
        Task<FridgeViewModel> GetAsync(string jwt);
        Task DeleteAsync(Guid fridgeId, string jwt);
        Task Add(string name, string typeId, string jwt);
    }
}
