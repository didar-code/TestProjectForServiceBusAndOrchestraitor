using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedSubSystem.Redis.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Redis.DependencyInjection
{
    public static class RedisDependencyInjection
    {
        public static IServiceCollection AddRedisCache(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("Redis");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Redis connection string is not configured.");
            }

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(
                    connectionString));

            services.AddScoped<
                IRedisCacheService,
                RedisCacheService>();

            return services;
        }
    }
}
