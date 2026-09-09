using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Events;
using OrderManagementSubsystem.Repository.Interfaces;
using SharedSubSystem.Events;
using SharedSubSystem.Exceptions;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Commands
{
    public class ConfirmOrderHandler : ICommandHandler<ConfirmOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public ConfirmOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync(ConfirmOrderCommand command)
        {
            var order =
                await _repository.GetByIdAsync(
                    command.OrderId);

            if (order == null)
                throw new NotFoundException("Order not found.");

            order.Confirm();

            await _repository.SaveAsync();

            return new List<Event>
            {
                new OrderConfirmedEvent(
                    command.OrderId,
                    command.PaymentId)
            };
        }
    }
}
