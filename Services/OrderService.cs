using System;
using grpcService.Contracts;

namespace grpcService.Services;

public class OrderService : ServiceBase<Order>
{
    public OrderService(IRepository<Order> repository) : base(repository)
    {
    }
}
