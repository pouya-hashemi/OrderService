using Beta.OrderService.Application.Dtos;
using Beta.OrderService.Domain.Exceptions;
using Beta.OrderService.Domain.Interfaces;
using MediatR;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.ApplicationServices.Orders.Commands
{
    internal class CreateOrderCommand:IRequest<OrderDto>
    {
    }
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly ISqlDbContext _context;
        private readonly IOrderManager _orderManager;

        public CreateOrderHandler(ISqlDbContext context,
            IOrderManager orderManager)
        {
            this._context = context;
            this._orderManager = orderManager;
        }
        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order =await _orderManager.CreateOrderAsync();

            if (order == null)
            {
                throw new BadRequestException("Couldn't create order");
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            var orderDto = order.Adapt<OrderDto>();

            return orderDto;
        }
    }
}
