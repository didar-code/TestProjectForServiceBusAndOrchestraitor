using OrderManagementSubsystem.DTOs.Events;
using OrderManagementSubsystem.Handler.Interfaces;
using OrderManagementSubsystem.Repository.Interfaces;
using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Commands
{
    public class OrderCommandHandler : IOrderCommandHandler
    {
        private readonly IOrderRepository _repository;

        public OrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> ConfirmOrderAsync(int orderId, int paymentId)
        {
            var order =await _repository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            
            order.Confirm();

            await _repository.SaveAsync();

     
            return new List<Event>
            {
                new OrderConfirmedEvent(orderId, paymentId)
            };
        }
    }
}
