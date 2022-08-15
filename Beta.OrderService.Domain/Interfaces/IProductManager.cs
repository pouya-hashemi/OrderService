using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Interfaces
{
    internal interface IProductManager
    {
        Task ProductExists(long productId);
    }
}
