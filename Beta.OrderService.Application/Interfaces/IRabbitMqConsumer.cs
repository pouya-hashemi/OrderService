using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Interfaces
{
    public interface IRabbitMqConsumer
    {
        public void Consume<T>(T message)
                   where T : IRabbitMessageConsumer;
    }
}
