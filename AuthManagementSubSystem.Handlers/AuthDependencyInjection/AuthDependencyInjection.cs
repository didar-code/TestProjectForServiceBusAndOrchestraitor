using AuthManagementSubSystem.DTOs.Commands;
using AuthManagementSubSystem.Handlers.Commands;
using AuthManagementSubSystem.Repository.Data;
using AuthManagementSubSystem.Repository.Interfaces;
using AuthManagementSubSystem.Repository.Repositories;
using AuthManagementSubSystem.Repository.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedSubSystem.Generics;
using SharedSubSystem.Security;

namespace AuthManagementSubSystem.Handlers.DependencyInjection
{
    public static class AuthDependencyInjection
    {
        public static IServiceCollection AddAuthManagement(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AuthCon")));

            services.AddScoped<
                IUserRepository,
                UserRepository>();

            services.AddScoped<
                IPasswordHasher,
                PasswordHasher>();

            services.AddScoped<
                ITokenService,
                JwtTokenService>();

            services.AddScoped<
                ICommandHandler<RegisterUserCommand>,
                RegisterUserHandler>();

            services.AddScoped<
                ICommandHandler<LoginCommand>,
                LoginHandler>();

            return services;
        }
    }
}