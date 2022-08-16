using Beta.OrderService.Application.Interfaces;
using Beta.OrderService.Infrastructure.RabbitMq.ConsumerMessages;

namespace Beta.OrderService.WebApi.Common
{
    public class RabbitMqSubscriber : IHostedService
    {
        private readonly IRabbitMqConsumer _consumer;

        public RabbitMqSubscriber(IRabbitMqConsumer consumer)
        {
            this._consumer = consumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Consume(new ProductCreateMessage());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
