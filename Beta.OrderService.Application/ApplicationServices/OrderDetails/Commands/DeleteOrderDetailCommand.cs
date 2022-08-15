using Beta.OrderService.Domain.Exceptions;
using Beta.OrderService.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Application.ApplicationServices.OrderDetails.Commands;

public class DeleteOrderDetailCommand:IRequest
{
    public long OrderDetailId { get; set; }
}

public class DeleteOrderDetailHandler : IRequestHandler<DeleteOrderDetailCommand>
{
    private readonly ISqlDbContext _context;

    public DeleteOrderDetailHandler(ISqlDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail =
            await _context.OrderDetails.FirstOrDefaultAsync(dtl => dtl.Id == request.OrderDetailId,
                cancellationToken);

        if (orderDetail==null)
        {
            throw new NotFoundException("Order item was not found");
        }

        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}