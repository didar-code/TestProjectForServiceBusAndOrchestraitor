using OrderManagementSubsystem.Aggregators;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Events;
using OrderManagementSubsystem.Repository.Interfaces;
using SharedSubSystem.Events;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Commands
{
    public class CreateOrderHandler: ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync(CreateOrderCommand command)
        {
            var order = OrderAggregatorsRoot.Create(
                command.CustomerName,
                command.TotalAmount);

            await _repository.AddAsync(order);

            await _repository.SaveAsync();

            return new List<Event>
            {
                new OrderCreatedEvent
                {
                    OrderId = order.OrderId
                }
            };
        }
    }
}
