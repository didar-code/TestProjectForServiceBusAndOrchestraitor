using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.Handler.Commands;
using PaymentManagementSubSystem.Handler.Interfaces;
using PaymentManagementSubSystem.Handler.Queries;
using PaymentManagementSubSystem.Handler.Validators;
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
        public static IServiceCollection AddPaymentManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(
                        configuration.GetConnectionString("PaymentCon")));

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<CreatePaymentHandler>();
            services.AddScoped<ICommandHandler<CreatePaymentCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<CreatePaymentCommand>(
                    sp.GetRequiredService<CreatePaymentHandler>(),
                    sp.GetService<IValidator<CreatePaymentCommand>>()));

            services.AddScoped<ConfirmPaymentHandler>();
            services.AddScoped<ICommandHandler<ConfirmPaymentCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<ConfirmPaymentCommand>(
                    sp.GetRequiredService<ConfirmPaymentHandler>(),
                    sp.GetService<IValidator<ConfirmPaymentCommand>>()));

            services.AddScoped<FailPaymentHandler>();
            services.AddScoped<ICommandHandler<FailPaymentCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<FailPaymentCommand>(
                    sp.GetRequiredService<FailPaymentHandler>(),
                    sp.GetService<IValidator<FailPaymentCommand>>()));
            services.AddScoped<UpdatePaymentHandler>();
            services.AddScoped<ICommandHandler<UpdatePaymentCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<UpdatePaymentCommand>(
                    sp.GetRequiredService<UpdatePaymentHandler>(),
                    sp.GetService<IValidator<UpdatePaymentCommand>>()));

            services.AddScoped<ProcessPaymentHandler>();
            services.AddScoped<ICommandHandler<ProcessPaymentCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<ProcessPaymentCommand>(
                    sp.GetRequiredService<ProcessPaymentHandler>(),
                    sp.GetService<IValidator<ProcessPaymentCommand>>()));
            services.AddScoped<GetPaymentHandler>();

            services.AddValidatorsFromAssemblyContaining<CreatePaymentCommandValidator>();

            return services;
        }
    }
}
