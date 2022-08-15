using Beta.OrderService.Application.Common;
using Beta.OrderService.Application.Dtos;
using Beta.OrderService.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.ApplicationServices.Orders.Queries
{
    public class GetOrdersQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long? OrderId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? OrderNumber { get; set; }
    }
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PaginatedList<OrderDto>>
    {
        private readonly ISqlDbContext _context;

        public GetOrdersHandler(ISqlDbContext context)
        {
            this._context = context;
        }
        public async Task<PaginatedList<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            request.PageSize=request.PageSize==0?10:request.PageSize;
            request.PageIndex=request.PageIndex == 0?1:request.PageIndex;

            var query = _context.Orders.AsQueryable();

            if (request.FromDate != null)
            {
                query = query.Where(w => w.SubmitDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                query = query.Where(w => w.SubmitDate <= request.ToDate);
            }

            if (request.OrderNumber != null)
            {
                query = query.Where(w => w.OrderNumber == request.OrderNumber);
            }
            
            if (request.OrderId != null)
            {
                query = query.Where(w => w.Id == request.OrderId);
            }

            var list = await query
                .OrderByDescending(o => o.SubmitDate)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new OrderDto()
                {
                    Id = s.Id,
                    OrderNumber = s.OrderNumber,
                    SubmitDate = s.SubmitDate,
                })
                .ToListAsync();

            var result = new PaginatedList<OrderDto>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                List = list,
                TotalRows = await query.CountAsync()
            };

            return result;



        }
    }
}
