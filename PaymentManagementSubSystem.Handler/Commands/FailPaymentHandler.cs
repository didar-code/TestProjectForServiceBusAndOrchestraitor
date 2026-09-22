using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Events;
using PaymentManagementSubSystem.Repository.Interfaces;
using SharedSubSystem.Events;
using SharedSubSystem.Exceptions;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Commands
{
    public class FailPaymentHandler : ICommandHandler<FailPaymentCommand>
    {
        private readonly IPaymentRepository _repository;

        public FailPaymentHandler(
            IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync( FailPaymentCommand command)
        {
            var payment =
                await _repository.GetByIdAsync(
                    command.PaymentId);

            if (payment == null)
                throw new NotFoundException("Payment not found.");

            payment.Fail();

            await _repository.SaveAsync();

            return new List<Event>
            {
                new PaymentFailedEvent(
                    payment.PaymentId,
                    payment.OrderId)
            };
        }
    }
}
