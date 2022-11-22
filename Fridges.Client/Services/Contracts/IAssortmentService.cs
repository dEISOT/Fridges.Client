using Fridges.Client.Models;

namespace Fridges.Client.Services.Contracts
{
    public interface IAssortmentService
    {
        Task<IEnumerable<Assortment>> GetProductsAsync(Guid fridgeId);
        Task DeleteAsync(string assormentId);
        Task DeleteAllAsync(string fridgeId);

    }
}
