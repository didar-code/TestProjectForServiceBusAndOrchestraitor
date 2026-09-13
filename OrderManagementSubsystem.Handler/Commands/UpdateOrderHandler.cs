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
    public class UpdateOrderHandler
        : ICommandHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync(UpdateOrderCommand command)
        {
            var order = await _repository.GetByIdAsync(
                command.OrderId);

            if (order == null)
                throw new NotFoundException ("Order not found.");
         

            order.Update(
                command.CustomerName,
                command.TotalAmount);

            await _repository.SaveAsync();

            return new List<Event>
            {
                new OrderUpdatedEvent
                {
                    OrderId = order.OrderId,
                    CustomerName = order.CustomerName,
                    TotalAmount = order.TotalAmount
                }
            };
        }
    }
}
