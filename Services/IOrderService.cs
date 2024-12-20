using System;

namespace grpcService.Services;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(string id);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order);
    bool DeleteOrderAsync(string id);
}
