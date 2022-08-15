using Beta.OrderService.Application.Dtos;
using Beta.OrderService.Domain.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.ApplicationServices.OrderDetails.Commands
{
    public class CreateOrderDetailCommand:IRequest<OrderDetailDto>
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderDetailHandler : IRequestHandler<CreateOrderDetailCommand, OrderDetailDto>
    {
        private readonly ISqlDbContext _sqlDbContext;
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly IOrderManager _orderManager;
        private readonly IProductManager _productManager;

        public CreateOrderDetailHandler(ISqlDbContext sqlDbContext,
            IOrderDetailManager orderDetailManager,
            IOrderManager orderManager,
            IProductManager productManager)
        {
            this._sqlDbContext = sqlDbContext;
            this._orderDetailManager = orderDetailManager;
            this._orderManager = orderManager;
            this._productManager = productManager;
        }
        public async Task<OrderDetailDto> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            await _orderManager.OrderExistsAsync(request.OrderId);
            await _productManager.ProductExistsAsync(request.ProductId);

            var orderDetail = await _orderDetailManager.CreateOrderDetailAsync(request.OrderId, request.ProductId, request.Quantity);

            _sqlDbContext.OrderDetails.Add(orderDetail);
            await _sqlDbContext.SaveChangesAsync(cancellationToken);

            var orderDetailDto = orderDetail.Adapt<OrderDetailDto>();

            return orderDetailDto;
        }
    }
}
