using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Dtos
{
    public class OrderDto
    {
        public long Id { get; set; }
        public long OrderNumber { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}
