using Beta.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Interfaces
{
    public interface IOrderManager
    {
        Task<Order> CreateOrderAsync();
        Task OrderExistsAsync(long orderId);
        Task CanDeleteOrderAsync(Order order);
        Task<bool> OrderHasDetailsAsync(long orderId);
    }
}
