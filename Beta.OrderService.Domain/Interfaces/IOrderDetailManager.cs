using Beta.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Interfaces
{
    internal interface IOrderDetailManager
    {
        Task<OrderDetail> CreateOrderDetail(long orderId, long productId, int quantity);
        Task ProductExistsInOrder(long orderId, long productId);
    }
}
