using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentManagementSubSystem.DTOs.Commands;
using SharedSubSystem.Generics;

namespace PaymentManagementSubSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ICommandHandler<CreatePaymentCommand>_createHandler;

        private readonly ICommandHandler<ConfirmPaymentCommand>_confirmHandler;

        private readonly ICommandHandler<FailPaymentCommand>_failHandler;

        public PaymentController( ICommandHandler<CreatePaymentCommand> createHandler,ICommandHandler<ConfirmPaymentCommand> confirmHandler,
            ICommandHandler<FailPaymentCommand> failHandler)
        {
            _createHandler = createHandler;
            _confirmHandler = confirmHandler;
            _failHandler = failHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentCommand command)
        {
            var events =
                await _createHandler.HandleAsync(command);

            return Ok(events);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm(ConfirmPaymentCommand command)
        {
            var events =
                await _confirmHandler.HandleAsync(command);

            return Ok(events);
        }

        [HttpPost("fail")]
        public async Task<IActionResult> Fail(FailPaymentCommand command)
        {
            var events =
                await _failHandler.HandleAsync(command);

            return Ok(events);
        }
    }
}
