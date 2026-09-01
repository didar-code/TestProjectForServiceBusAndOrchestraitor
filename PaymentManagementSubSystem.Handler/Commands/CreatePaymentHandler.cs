using PaymentManagementSubSystem.Aggregators;
using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Events;
using PaymentManagementSubSystem.Repository.Interfaces;
using SharedSubSystem.Events;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Commands
{
    public class CreatePaymentHandler : ICommandHandler<CreatePaymentCommand>
    {
        private readonly IPaymentRepository _repository;

        public CreatePaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync(CreatePaymentCommand command)
        {
            var payment = PaymentAggregatorsRoot.Create(command.OrderId,
                command.Amount,
                command.PaymentMethod);

            await _repository.AddAsync(payment);

            await _repository.SaveAsync();

            return new List<Event>
            {
                new PaymentCreatedEvent
                {
                    PaymentId = payment.PaymentId,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount
                }
            };
        }
    }
}
