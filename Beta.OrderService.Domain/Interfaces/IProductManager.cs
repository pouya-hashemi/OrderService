using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Interfaces
{
    public interface IProductManager
    {
        Task ProductExistsAsync(long productId);
    }
}
