using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Domain;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _dbContext;

        public OrderService(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Orders.ToListAsync(cancellationToken);
        }

        public async Task<Order> CreateOrderAsync(decimal totalAmount, CancellationToken cancellationToken)
        {
            var order = new Order(totalAmount);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}