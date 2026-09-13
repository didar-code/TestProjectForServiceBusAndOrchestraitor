using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Responses;
using PaymentManagementSubSystem.Handler.Queries;
using SharedSubSystem.Generics;

namespace PaymentManagementSubSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ICommandHandler<CreatePaymentCommand> _createHandler;

        private readonly ICommandHandler<ConfirmPaymentCommand> _confirmHandler;

        private readonly ICommandHandler<FailPaymentCommand> _failHandler;

        private readonly ICommandHandler<UpdatePaymentCommand> _updateHandler;

        private readonly GetPaymentHandler _getPaymentHandler;


        public PaymentController(
            ICommandHandler<CreatePaymentCommand> createHandler,
            ICommandHandler<ConfirmPaymentCommand> confirmHandler,
            ICommandHandler<FailPaymentCommand> failHandler,
            ICommandHandler<UpdatePaymentCommand> updateHandler,
            GetPaymentHandler getPaymentHandler)
        {
            _createHandler = createHandler;
            _confirmHandler = confirmHandler;
            _failHandler = failHandler;
            _updateHandler = updateHandler;
            _getPaymentHandler = getPaymentHandler;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePaymentCommand command)
        {
            var events =
                await _createHandler.HandleAsync(command);

            return Ok(events);
        }


        [HttpPut("{paymentId}")]
        [Authorize]
        public async Task<IActionResult> Update(int paymentId,
            [FromBody] UpdatePaymentCommand command)
        {
            command.PaymentId = paymentId;

            var result =
                await _updateHandler.HandleAsync(command);

            return Ok(result);
        }


        [HttpGet("{paymentId:int}")]
        public async Task<IActionResult> GetById(int paymentId)
        {
            if (paymentId <= 0)
                return BadRequest(
                    "Payment id must be greater than zero.");

            var result =
                await _getPaymentHandler.GetByIdAsync(paymentId);

            if (result == null)
                return NotFound("Payment not found.");

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _getPaymentHandler.GetAllAsync();

            return Ok(result);
        }


        // [HttpPost("confirm")]
        // public async Task<IActionResult> Confirm(
        //     ConfirmPaymentCommand command)
        // {
        //     var events =
        //         await _confirmHandler.HandleAsync(command);

        //     return Ok(events);
        // }


        // [HttpPost("fail")]
        // public async Task<IActionResult> Fail(
        //     FailPaymentCommand command)
        // {
        //     var events =
        //         await _failHandler.HandleAsync(command);

        //     return Ok(events);
        // }
    }
}
