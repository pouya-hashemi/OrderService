using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message):base(message)
        {
            this.Data.Add("status", (int)HttpStatusCode.NotFound);
        }
    }
}
