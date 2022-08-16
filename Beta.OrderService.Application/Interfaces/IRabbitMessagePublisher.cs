using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Interfaces
{
    public interface IRabbitMessagePublisher:IRabbitMessage
    {
        public object MessageData { get; }
    }
}
