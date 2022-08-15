using Beta.OrderService.Domain.Exceptions;
using Beta.OrderService.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.ApplicationServices.Orders.Commands
{
    public class DeleteOrderCommand:IRequest
    {
        public long OrderId { get; set; }
    }
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly ISqlDbContext _context;
        private readonly IOrderManager _orderManager;

        public DeleteOrderHandler(ISqlDbContext context,
            IOrderManager orderManager)
        {
            this._context = context;
            this._orderManager = orderManager;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order=await  _context.Orders.FirstOrDefaultAsync(ord => ord.Id == request.OrderId);
            if (order == null)
            {
                throw new NotFoundException("order was not found");
            }
            await _orderManager.CanDeleteOrderAsync(order);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
