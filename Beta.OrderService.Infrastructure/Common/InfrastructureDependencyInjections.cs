using Beta.OrderService.Application.Interfaces;
using Beta.OrderService.Domain.Interfaces;
using Beta.OrderService.Infrastructure.Persistance.SqlServer;
using Beta.OrderService.Infrastructure.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Infrastructure.Common
{
    public static class InfrastructureDependencyInjections
    {
        public static IServiceCollection AddInferastructureInjections(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<SqlDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });

            services.AddScoped<ISqlDbContext, SqlDbContext>();
            services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();


            return services;
        }
    }
}
