using Beta.OrderService.Domain.Entities;
using Beta.OrderService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Domain.Managers;

public class OrderManager
{
    private readonly ISqlDbContext _context;

    public OrderManager(ISqlDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrder()
    {
        var maxOrderNumber = await _context.Orders
            .OrderByDescending(o => o.OrderNumber)
            .Select(s => s.OrderNumber)
            .FirstOrDefaultAsync();

        var newOrderNumber=  maxOrderNumber == 0 ? 100001 : maxOrderNumber + 1;

        var order = new Order(newOrderNumber);
        return order;
    }
}