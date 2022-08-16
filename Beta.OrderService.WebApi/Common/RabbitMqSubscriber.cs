using Beta.OrderService.Application.Interfaces;
using Beta.OrderService.Infrastructure.RabbitMq.ConsumerMessages;
using MediatR;

namespace Beta.OrderService.WebApi.Common
{
    public class RabbitMqSubscriber : IHostedService
    {
        private readonly IRabbitMqConsumer _consumer;
        private readonly ISender sender;

        public RabbitMqSubscriber(IRabbitMqConsumer consumer,
            ISender sender)
        {
            this._consumer = consumer;
            this.sender = sender;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Consume(new ProductCreateMessage(sender));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
