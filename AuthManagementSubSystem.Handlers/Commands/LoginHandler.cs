using AuthManagementSubSystem.DTOs.Commands;
using AuthManagementSubSystem.DTOs.Events;
using AuthManagementSubSystem.Repository.Interfaces;
using AuthManagementSubSystem.Repository.Security;
using SharedSubSystem.Events;
using SharedSubSystem.Generics;
using SharedSubSystem.Security;

namespace AuthManagementSubSystem.Handlers.Commands
{
    public class LoginHandler : ICommandHandler<LoginCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUserRepository repository,IPasswordHasher passwordHasher,ITokenService tokenService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<Event>> HandleAsync(LoginCommand command)
        {
            var user =
                await _repository.GetByEmailAsync(
                    command.Email);

            if (user == null)
                throw new Exception(
                    "Invalid email or password.");

            if (!user.IsActive)
                throw new Exception(
                    "User account is inactive.");

            var passwordValid =
                _passwordHasher.Verify(
                    command.Password,
                    user.PasswordHash);

            if (!passwordValid)
                throw new Exception("Invalid email or password.");

            var token =
                _tokenService.GenerateToken(user.UserId,user.Email,user.Role);

            return new List<Event>
            {
                new UserLoggedInEvent
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role,
                    Token = token
                }
            };
        }
    }
}