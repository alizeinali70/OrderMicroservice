using InventoryService.Domain;

namespace InventoryService.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<Inventory> GetInventoryByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
