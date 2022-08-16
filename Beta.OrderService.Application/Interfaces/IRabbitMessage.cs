using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Interfaces
{
    public interface IRabbitMessage
    {
        public string ExchangeName { get; }
        public string ExchangeType { get; }

    }
}
