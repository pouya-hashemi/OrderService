using Beta.OrderService.Application.ApplicationServices.Products.Commands;
using Beta.OrderService.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Infrastructure.RabbitMq.ConsumerMessages
{
    public class ProductCreateMessage : IRabbitMessageConsumer
    {
        public ProductCreateMessage(IServiceProvider serviceProvider)
        {
            

            QueueName = "CreateProduct_OrderService";
            ExchangeName = "CreateProduct";
            ExchangeType = RabbitMQ.Client.ExchangeType.Fanout;

            this.EventHandler =async (sender, e) =>
            {

                
                var bodyString = Encoding.UTF8.GetString(e.Body.ToArray());

                var createProductCommand = JsonConvert.DeserializeObject<CreateProductCommand>(bodyString);
                if (createProductCommand != null)
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediatorSender =
                            scope.ServiceProvider
                                .GetRequiredService<ISender>();
                        await mediatorSender.Send(createProductCommand);
                    }
                    
                }

            };
        }
        public string QueueName { get; private set; }

        public EventHandler<BasicDeliverEventArgs> EventHandler { get; set; }

        public string ExchangeName { get; private set; }

        public string ExchangeType { get; private set; }



    }
}
