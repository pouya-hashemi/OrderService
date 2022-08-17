using Beta.OrderService.Application.ApplicationServices.Products.Commands;
using Beta.OrderService.Application.Interfaces;
using Beta.OrderService.Infrastructure.RabbitMq.ConsumerMessages;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Beta.OrderService.WebApi.Common
{
    public class RabbitMqSubscriber : IHostedService
    {
        private readonly IRabbitMqConsumer _rabbitMqConsumer;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqSubscriber(IRabbitMqConsumer rabbitMqConsumer,
            IServiceProvider serviceProvider)
        {
            this._rabbitMqConsumer = rabbitMqConsumer;
            this._serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _rabbitMqConsumer.Consume(new ProductCreateMessage(_serviceProvider));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
