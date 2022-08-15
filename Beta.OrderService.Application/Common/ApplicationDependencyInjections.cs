using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.Common
{
    public static class ApplicationDependencyInjections
    {
        public static IServiceCollection AddApplicationInjections(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(ApplicationDependencyInjections)));

            return services;
        }
    }
}
