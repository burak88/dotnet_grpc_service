using System;
using grpcService.Models;

namespace grpcService.Repositories;

public class RepositoryOrder : RepositoryBase<Order>
{
    public RepositoryOrder(AppDbContext dbContext) : base(dbContext)
    {
    }
}
