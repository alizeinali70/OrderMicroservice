using InventoryService.Application.Interfaces;
using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly DbContext _dbContext;

        public InventoryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Inventory?> GetInventoryByProductIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Inventory>()
                .FirstOrDefaultAsync(i => i.ProductId == productId, cancellationToken);
        }


    }
}
