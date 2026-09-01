using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrchestraitorSubSystem.DTOs.Command;
using OrchestraitorSubSystem.Handler.Orchestraitators;

namespace OrchestraitorSubSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrchestratorController : ControllerBase
    {
        private readonly OrderPaymentOrchestrator _orchestrator;

        public OrchestratorController( OrderPaymentOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [HttpPost("process-order-payment")]
        public async Task<IActionResult> ProcessOrderPayment( [FromBody] ProcessOrderPaymentCommand command)
        {
            try
            {
                var result =
                    await _orchestrator.ProcessAsync(command);

                if (!result)
                {
                    return BadRequest(new
                    {
                        Message = "Order payment process failed."
                    });
                }

                return Ok(new
                {
                    Message = "Order payment processed successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
