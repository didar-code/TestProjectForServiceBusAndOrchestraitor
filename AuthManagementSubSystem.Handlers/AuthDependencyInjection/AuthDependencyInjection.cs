using AuthManagementSubSystem.DTOs.Commands;
using AuthManagementSubSystem.Handlers.Commands;
using AuthManagementSubSystem.Handlers.Validators;
using AuthManagementSubSystem.Repository.Data;
using AuthManagementSubSystem.Repository.Interfaces;
using AuthManagementSubSystem.Repository.Repositories;
using AuthManagementSubSystem.Repository.Security;
using FluentValidation;
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

            services.AddScoped
                <IUserRepository,
                UserRepository > ();

            services.AddScoped
                <IPasswordHasher,
                PasswordHasher > ();

            services.AddScoped
                <ITokenService,
                JwtTokenService > ();

            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddScoped<ICommandHandler<RegisterUserCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<RegisterUserCommand>(
                    sp.GetRequiredService<RegisterUserHandler>()));

            services.AddScoped<LoginHandler>();
            services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
            services.AddScoped<ICommandHandler<LoginCommand>>(sp =>
                new ValidatingCommandHandlerDecorator<LoginCommand>(
                    sp.GetRequiredService<LoginHandler>()));

            return services;
        }
    }
}