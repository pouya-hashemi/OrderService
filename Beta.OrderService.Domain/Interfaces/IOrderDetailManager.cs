using Beta.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Interfaces
{
    public interface IOrderDetailManager
    {
        Task<OrderDetail> CreateOrderDetailAsync(long orderId, long productId, int quantity);
        Task ProductExistsInOrderAsync(long orderId, long productId);
        void SetQuantity(OrderDetail orderDetail, int quantity);
    }
}
