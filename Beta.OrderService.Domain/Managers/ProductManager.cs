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
    internal class ProductManager: IProductManager
    {
        private readonly ISqlDbContext _context;

        public ProductManager(ISqlDbContext context)
        {
            this._context = context;
        }


        public async Task ProductExists(long productId)
        {
            var exists = await _context.Products.AnyAsync(a => a.Id == productId);
            if (!exists)
            {
                throw new NotFoundException("Product was not found");
            }
        }
    }
}
