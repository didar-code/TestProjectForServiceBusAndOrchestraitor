using AuthManagementSubSystem.Aggregators;
using AuthManagementSubSystem.DTOs.Commands;
using AuthManagementSubSystem.DTOs.Events;
using AuthManagementSubSystem.Repository.Interfaces;
using AuthManagementSubSystem.Repository.Security;
using Microsoft.AspNetCore.Identity;
using SharedSubSystem.Events;
using SharedSubSystem.Generics;
using SharedSubSystem.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagementSubSystem.Handlers.Commands
{
    public class RegisterUserHandler: ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUserRepository repository,IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<Event>> HandleAsync(RegisterUserCommand command)
        {
            var exists =
                await _repository.ExistsByEmailAsync(
                    command.Email);

            if (exists)
                throw new Exception(
                    "Email already exists.");

            var passwordHash =
                _passwordHasher.Hash(
                    command.Password);

            var user =
                UserAggregatorsRoot.Create(
                    command.UserName,
                    command.Email,
                    passwordHash);

            await _repository.AddAsync(user);

            await _repository.SaveAsync();

            return new List<Event>
            {
                new UserRegisteredEvent
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }
    }
}
