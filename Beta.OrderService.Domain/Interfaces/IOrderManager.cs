using Beta.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Interfaces
{
    internal interface IOrderManager
    {
        Task<Order> CreateOrder();
        Task OrderExists(long orderId);
    }
}
