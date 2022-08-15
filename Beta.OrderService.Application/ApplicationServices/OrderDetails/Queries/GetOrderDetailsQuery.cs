using Beta.OrderService.Application.Common;
using Beta.OrderService.Application.Dtos;
using Beta.OrderService.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Beta.OrderService.Application.ApplicationServices.OrderDetails.Queries;

public class GetOrderDetailsQuery:IRequest<PaginatedList<OrderDetailDto>>
{
    public long? OrderDetailId { get; set; }
    public long? OrderId { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    
}

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, PaginatedList<OrderDetailDto>>
{
    private readonly ISqlDbContext _context;

    public GetOrderDetailsHandler(ISqlDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<OrderDetailDto>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        request.PageSize=request.PageSize==0?10:request.PageSize;
        request.PageIndex=request.PageIndex == 0?1:request.PageIndex;
        
        var query = _context.OrderDetails
            .Include(i=>i.Product)
            .AsQueryable();

        if (request.OrderId != null)
        {
            query = query.Where(w => w.OrderId == request.OrderId);
        }
        
        if (request.OrderDetailId != null)
        {
            query = query.Where(w => w.Id == request.OrderDetailId);
        }
        
        var list = await query
            .OrderByDescending(o => o.Id)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => new OrderDetailDto()
            {
                Id = s.Id,
                ProductId = s.ProductId,
                ProductName = s.Product.Name,
                OrderId = s.OrderId,
                Quantiy = s.Quantity
            })
            .ToListAsync(cancellationToken);
        
        var result = new PaginatedList<OrderDetailDto>()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            List = list,
            TotalRows = await query.CountAsync(cancellationToken)
        };

        return result; 
    }
}