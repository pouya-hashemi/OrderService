using Beta.OrderService.Domain.Entities;
using Beta.OrderService.Domain.Exceptions;
using Beta.OrderService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Managers
{
    public class OrderDetailManager: IOrderDetailManager
    {
        private readonly ISqlDbContext _context;
        private readonly IOrderManager _orderManager;
        private readonly IProductManager _productManager;

        public OrderDetailManager(ISqlDbContext context,
            IOrderManager orderManager,
            IProductManager productManager)
        {
            this._context = context;
            this._orderManager = orderManager;
            this._productManager = productManager;
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(long orderId,long productId,int quantity)
        {
            await _orderManager.OrderExistsAsync(orderId);
            await _productManager.ProductExistsAsync(productId);
            await ProductExistsInOrderAsync(orderId, productId);

            var orderDetail = new OrderDetail(orderId, productId, quantity);

            return orderDetail;

        }

        public async Task ProductExistsInOrderAsync(long orderId,long productId)
        {
            var exists = await _context.OrderDetails.AnyAsync(a => a.OrderId == orderId && a.ProductId == productId);
            if (exists)
            {
                throw new BadRequestException("Product already exists in this order");
            }
        }

        public void SetQuantity(OrderDetail orderDetail, int quantity)
        {
            orderDetail.SetQuantity(quantity);
        }
        
    }
}
