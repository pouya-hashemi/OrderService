using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Interfaces
{
    public interface IRabbitMessageConsumer:IRabbitMessage
    {
        public string QueueName { get; }
        public EventHandler<BasicDeliverEventArgs> EventHandler { get; set; }
    }
}
