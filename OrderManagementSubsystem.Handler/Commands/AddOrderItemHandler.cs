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
    public class AddOrderItemHandler : ICommandHandler<AddOrderItemCommand>
    {
        private readonly IOrderRepository _repository;

        public AddOrderItemHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync(AddOrderItemCommand command)
        {
            var order = await _repository.GetByIdAsync(command.OrderId);

            if (order == null)
                throw new NotFoundException("Order not found.");

            order.AddItem(command.ProductName, command.Quantity, command.UnitPrice);

            await _repository.SaveAsync();

            return new List<Event>
            {
                new OrderItemAddedEvent
                {
                    OrderId = order.OrderId,
                    ProductName = command.ProductName,
                    Quantity = command.Quantity,
                    UnitPrice = command.UnitPrice,
                    NewTotalAmount = order.TotalAmount
                }
            };
        }
    }
}
