using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.Handler.Commands;
using PaymentManagementSubSystem.Handler.Interfaces;
using PaymentManagementSubSystem.Repository.Data;
using PaymentManagementSubSystem.Repository.Interfaces;
using PaymentManagementSubSystem.Repository.Repositories;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.DependencyInjection
{
    public static class PaymentDependencyInjection
    {
        public static IServiceCollection AddPaymentManagement(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PaymentDbContext>(
                options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString(
                            "PaymentCon")));

            services.AddScoped<
                IPaymentRepository,
                PaymentRepository>();

            services.AddScoped<
                ICommandHandler<CreatePaymentCommand>,
                CreatePaymentHandler>();

            services.AddScoped<
                ICommandHandler<ConfirmPaymentCommand>,
                ConfirmPaymentHandler>();

            services.AddScoped<
                ICommandHandler<FailPaymentCommand>,
                FailPaymentHandler>();

            services.AddScoped<
                ICommandHandler<ProcessPaymentCommand>,
                ProcessPaymentHandler>();

            return services;
        }
    }
}
