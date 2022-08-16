using Beta.OrderService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.ApplicationServices.Products.Commands
{
    public class CreateProductCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class CreateProductHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly ISqlDbContext _context;

        public CreateProductHandler(ISqlDbContext context)
        {
            this._context = context;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("--> inside handler");
            _context.Products.Add(new Domain.Entities.Product()
            {
                Id = request.Id,
                Name = request.Name,
            });
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
