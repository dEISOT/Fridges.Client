using Fridges.Client.Models;

namespace Fridges.Client.Services.Contracts
{
    public interface IAssortmentService
    {
        Task<AssortmentWithProduct> GetProductsAsync(Guid fridgeId);
        Task DeleteAsync(string assormentId);
        Task DeleteAllAsync(string fridgeId);
        Task AddAsync(string fridgeId, string productId, int quantity);
        Task UpdateAsync(string assortmentId, int newQuantity);
    }
}
