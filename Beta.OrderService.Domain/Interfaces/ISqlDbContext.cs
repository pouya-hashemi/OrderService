using Beta.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Domain.Interfaces;

public interface ISqlDbContext
{
    DbSet<Order> Orders { get; set; }
    DbSet<OrderDetail> OrderDetails { get; set; }
    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}