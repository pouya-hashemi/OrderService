using Beta.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Domain.Interfaces;

public interface ISqlDbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
}