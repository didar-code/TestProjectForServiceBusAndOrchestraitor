using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Queries;
using OrderManagementSubsystem.DTOs.Responses;
using OrderManagementSubsystem.Handler.Commands;
using OrderManagementSubsystem.Handler.Interfaces;
using OrderManagementSubsystem.Handler.Queries;
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
        public static IServiceCollection AddOrderManagement(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(configuration.GetConnectionString("OrderCon")));

            services.AddScoped<IOrderRepository,OrderRepository>();

            services.AddScoped<ICommandHandler<CreateOrderCommand>,CreateOrderHandler>();

            services.AddScoped<ICommandHandler<ConfirmOrderCommand>,ConfirmOrderHandler>();
            services.AddScoped<IQueryHandler< SearchOrderQuery,IEnumerable<OrderResponseDto>>,SearchOrderHandler>();

            return services;
        }
    }
}
