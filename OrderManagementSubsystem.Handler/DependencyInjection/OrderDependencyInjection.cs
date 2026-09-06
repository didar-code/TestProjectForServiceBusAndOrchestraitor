using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Queries;
using OrderManagementSubsystem.DTOs.Responses;
using OrderManagementSubsystem.Handler.Commands;

using OrderManagementSubsystem.Handler.Queries;
using OrderManagementSubsystem.Handler.Validators;
using OrderManagementSubsystem.Repository.Data;
using OrderManagementSubsystem.Repository.Interfaces;
using OrderManagementSubsystem.Repository.Repositories;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.DependencyInjection
{
    public static class OrderDependencyInjection
    {
        public static IServiceCollection AddOrderManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderCon")));

            services.AddScoped<IOrderRepository, OrderRepository>();

          
            services.AddScoped<CreateOrderHandler>();
            services.AddScoped<ICommandHandler<CreateOrderCommand>>(sp =>new ValidatingCommandHandlerDecorator<CreateOrderCommand>(
                    sp.GetRequiredService<CreateOrderHandler>(),
                    sp.GetService<IValidator<CreateOrderCommand>>()));

            services.AddScoped<ConfirmOrderHandler>();
            services.AddScoped<ICommandHandler<ConfirmOrderCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<ConfirmOrderCommand>(
                    sp.GetRequiredService<ConfirmOrderHandler>(),
                    sp.GetService<IValidator<ConfirmOrderCommand>>()));

            services.AddScoped<IQueryHandler<SearchOrderQuery, IEnumerable<OrderResponseDto>>, SearchOrderHandler>();
            services.AddScoped<UpdateOrderHandler>();

            services.AddScoped<ICommandHandler<UpdateOrderCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<UpdateOrderCommand>(
                    sp.GetRequiredService<UpdateOrderHandler>(),
                    sp.GetService<IValidator<UpdateOrderCommand>>()));


            services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

            return services;
        }
    }
}
