using Beta.OrderService.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Infrastructure.RabbitMq
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqConnectionProvider _connectionProvider;

        public RabbitMqConsumer(IConfiguration configuration,
            IRabbitMqConnectionProvider connectionProvider)
        {
            this._configuration = configuration;
            this._connectionProvider = connectionProvider;
        }
        public void Consume<T>(T message)
                    where T : IRabbitMessageConsumer
        {
             var channel = _connectionProvider.GetModel();

            channel.ExchangeDeclare(message.ExchangeName, message.ExchangeType);
            channel.QueueDeclare(message.QueueName, true, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += message.EventHandler;


            channel.QueueBind(message.QueueName, message.ExchangeName, String.Empty);
            channel.BasicConsume(message.QueueName, true, consumer);

        }

    }
}
