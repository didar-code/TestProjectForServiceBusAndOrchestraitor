using OrchestraitorSubSystem.DTOs.Command;
using OrderManagementSubsystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Commands;
using ServiceBus;


namespace OrchestraitorSubSystem.Handler.Orchestraitators
{
    public class OrderPaymentOrchestrator
    {
        private readonly IServiceBus _serviceBus;

        public OrderPaymentOrchestrator(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public async Task<bool> ProcessAsync(ProcessOrderPaymentCommand command)
        {
            try
            {
                var processPaymentCommand = new ProcessPaymentCommand(command.PaymentId);
                

                var paymentEvents = await _serviceBus.SendCommandAsync(processPaymentCommand);

                if (!paymentEvents.Any())
                    return false;

                var confirmOrderCommand = new ConfirmOrderCommand
                {
                    OrderId = command.OrderId,
                    PaymentId = command.PaymentId
                };

                var orderEvents = await _serviceBus.SendCommandAsync(confirmOrderCommand);   

                if (orderEvents.Any())
                    return true;

                var failPaymentCommand = new FailPaymentCommand
                {
                    PaymentId = command.PaymentId
                };

                await _serviceBus.SendCommandAsync(failPaymentCommand);

                return false;
            }
            catch
            {
                var failPaymentCommand = new FailPaymentCommand
                {
                    PaymentId = command.PaymentId
                };

                await _serviceBus.SendCommandAsync(failPaymentCommand);

                return false;
            }
        }
    }
}
