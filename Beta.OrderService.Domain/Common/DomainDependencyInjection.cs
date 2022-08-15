using Beta.OrderService.Domain.Interfaces;
using Beta.OrderService.Domain.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Domain.Common
{
    public static class DomainDependencyInjection
    {
        public static IServiceCollection AddDomainInjections(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrderDetailManager, OrderDetailManager>();

            return services;
        }
    }
}
