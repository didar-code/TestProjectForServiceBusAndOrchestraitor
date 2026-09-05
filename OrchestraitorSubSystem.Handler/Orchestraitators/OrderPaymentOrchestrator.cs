using OrchestraitorSubSystem.DTOs.Command;
using OrderManagementSubsystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Commands;
using ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var paymentEvents = await _serviceBus.SendCommandAsync(
                    new ProcessPaymentCommand(command.PaymentId));

                if (!paymentEvents.Any())
                    return false;

                var orderEvents =await _serviceBus.SendCommandAsync(
                        new ConfirmOrderCommand
                        {
                            OrderId = command.OrderId,
                            PaymentId = command.PaymentId
                        });

                if (orderEvents.Any())
                    return true;

                await _serviceBus.SendCommandAsync(
                    new FailPaymentCommand
                    {
                        PaymentId = command.PaymentId
                    });

                return false;
            }
            catch
            {
                await _serviceBus.SendCommandAsync(
                    
                    new FailPaymentCommand
                    {
                        PaymentId = command.PaymentId
                    });

                return false;
            }
        }
    }
}
