using Beta.OrderService.Domain.Entities;
using Beta.OrderService.Domain.Exceptions;
using Beta.OrderService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Domain.Managers;

public class OrderManager: IOrderManager
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
    public async Task OrderExists(long orderId)
    {
        var exists = await _context.Orders.AnyAsync(a => a.Id == orderId);
        if (!exists)
        {
            throw new NotFoundException("Order was not found");
        }
    }
}