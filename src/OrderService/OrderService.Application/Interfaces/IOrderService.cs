using OrderService.Domain;

namespace OrderService.Application
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<Order> CreateOrderAsync(decimal totalAmount, CancellationToken cancellationToken);
    }
}