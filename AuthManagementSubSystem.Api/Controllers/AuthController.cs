using AuthManagementSubSystem.DTOs.Commands;
using AuthManagementSubSystem.DTOs.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedSubSystem.Generics;

namespace AuthManagementSubSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterUserCommand> _registerHandler;
        private readonly ICommandHandler<LoginCommand> _loginHandler;

        public AuthController(
            ICommandHandler<RegisterUserCommand> registerHandler,
            ICommandHandler<LoginCommand> loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            try
            {
                var events =
                    await _registerHandler.HandleAsync(command);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(
     [FromBody] LoginCommand command)
        {
            try
            {
                var events =
                    await _loginHandler.HandleAsync(command);

                var loginEvent =
                    events.OfType<UserLoggedInEvent>()
                          .FirstOrDefault();

                if (loginEvent == null)
                {
                    return Unauthorized(new
                    {
                        message = "Login failed."
                    });
                }

                return Ok(new
                {
                    userId = loginEvent.UserId,
                    userName = loginEvent.UserName,
                    email = loginEvent.Email,
                    role = loginEvent.Role,
                    token = loginEvent.Token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
