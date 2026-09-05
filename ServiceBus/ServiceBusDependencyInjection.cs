using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.Handler.Commands;
using OrderManagementSubsystem.Handler.DependencyInjection;

using PaymentManagementSubSystem.Handler.Commands;
using PaymentManagementSubSystem.Handler.DependencyInjection;
using PaymentManagementSubSystem.Handler.Interfaces;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public static class ServiceBusDependencyInjection
    {
        public static IServiceCollection AddServiceBusDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOrderManagement(configuration);

            services.AddPaymentManagement(configuration);

            services.AddScoped<IServiceBus, ServiceBus>();

            return services;
        }
    }
}
