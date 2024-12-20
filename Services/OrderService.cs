using System;
using grpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace grpcService.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    public OrderService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> CreateOrderAsync(Order order)
    {
        if (order is not null)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
        return order;
    }

    public bool DeleteOrderAsync(string id)
    {
        var orderObject = _dbContext.Orders.FirstOrDefault(c => c.Id == id);
        if (orderObject is not null)
        {
            _dbContext.Orders.Remove(orderObject);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public async Task<Order?> GetOrderByIdAsync(string id)
    {

        var orderById = await _dbContext.Orders.FirstOrDefaultAsync(c => c.Id == id);

        if (orderById == null)
        {
            throw new KeyNotFoundException($"Order with ID '{id}' not found.");
        }

        return orderById;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _dbContext.Orders
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        var orderObject = _dbContext.Orders.FirstOrDefault(c => c.Id == order.Id);
        if (orderObject is not null)
        {
            orderObject.Name = order.Name;
            orderObject.Enable = order.Enable;
            await _dbContext.SaveChangesAsync();
        }
        return order;
    }
}
