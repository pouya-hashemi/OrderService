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
    public class RabbitMqConsumer: IRabbitMqConsumer
    {
        private readonly IConfiguration _configuration;

        public RabbitMqConsumer(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void Consume<T>(T message)
                    where T : IRabbitMessageConsumer
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration["RabbitMqConnection"])
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(message.ExchangeName, message.ExchangeType);
            channel.QueueDeclare(message.QueueName, true, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += message.EventHandler;

            channel.QueueBind(message.QueueName, message.ExchangeName, String.Empty);
            channel.BasicConsume(message.QueueName, true, consumer);

        }
        
    }
}
