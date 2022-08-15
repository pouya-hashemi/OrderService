using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Dtos
{
    public class OrderDetailDto
    {
        public long Id { get; set; }
        public long OrderId { get;  set; }
        public long ProductId { get;  set; }
        public string ProductName { get;  set; }
        public int Quantiy { get;  set; }
    }
}
