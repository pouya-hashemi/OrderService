using Beta.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Domain.Interfaces;

public interface ISqlDbContext
{
    public DbSet<Order> Orders { get; set; }
}