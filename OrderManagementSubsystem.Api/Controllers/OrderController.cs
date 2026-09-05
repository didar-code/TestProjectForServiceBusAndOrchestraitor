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

        public OrderController(ICommandHandler<CreateOrderCommand> createHandler, IQueryHandler<SearchOrderQuery, IEnumerable<OrderResponseDto>> searchHandler)
        {
            _createHandler = createHandler;
            _searchHandler = searchHandler;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create( CreateOrderCommand command)
        {
            var events = await _createHandler.HandleAsync(command);

            return Ok(events);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchOrderQuery query)
        {
            var result =await _searchHandler.HandleAsync(query);

            return Ok(result);
        }
    }
}
