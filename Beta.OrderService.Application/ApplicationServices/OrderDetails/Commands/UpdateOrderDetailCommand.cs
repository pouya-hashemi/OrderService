using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beta.OrderService.Domain.Exceptions;
using Beta.OrderService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Application.ApplicationServices.OrderDetails.Commands
{
    public class UpdateOrderDetailCommand:IRequest
    {
        public long OrderDetailId { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateOrderDetailHandler : IRequestHandler<UpdateOrderDetailCommand>
    {
        private readonly ISqlDbContext _context;
        private readonly IOrderDetailManager _orderDetailManager;

        public UpdateOrderDetailHandler(ISqlDbContext context,
            IOrderDetailManager orderDetailManager)
        {
            _context = context;
            _orderDetailManager = orderDetailManager;
        }
        public async Task<Unit> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail =
                await _context.OrderDetails.FirstOrDefaultAsync(dtl => dtl.Id == request.OrderDetailId,
                    cancellationToken);

            if (orderDetail==null)
            {
                throw new NotFoundException("Order item was not found");
            }
            _orderDetailManager.SetQuantity(orderDetail,request.Quantity);

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
