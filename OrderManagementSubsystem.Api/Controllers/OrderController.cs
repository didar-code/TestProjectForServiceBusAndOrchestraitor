using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Queries;
using OrderManagementSubsystem.DTOs.Responses;
using SharedSubSystem.Generics;

namespace OrderManagementSubsystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class OrderController : ControllerBase
    {
        private readonly ICommandHandler<CreateOrderCommand> _createHandler;
        private readonly IQueryHandler<SearchOrderQuery, IEnumerable<OrderResponseDto>> _searchHandler;
        private readonly ICommandHandler<UpdateOrderCommand> _updateHandler;

        public OrderController(ICommandHandler<CreateOrderCommand> createHandler, IQueryHandler<SearchOrderQuery, 
            IEnumerable<OrderResponseDto>> searchHandler, ICommandHandler<UpdateOrderCommand> updateHandler)
        {
            _createHandler = createHandler;
            _searchHandler = searchHandler;
            _updateHandler = updateHandler;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create( CreateOrderCommand command)
        {
            var events = await _createHandler.HandleAsync(command);

            return Ok(events);
        }

        [HttpPut("{orderId:int}")]
        public async Task<IActionResult> Update(
           int orderId,
           [FromBody] UpdateOrderCommand command)
        {
            command.OrderId = orderId;

            var result = await _updateHandler.HandleAsync(command);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchOrderQuery query)
        {
            var result =await _searchHandler.HandleAsync(query);

            return Ok(result);
        }
    }
}
