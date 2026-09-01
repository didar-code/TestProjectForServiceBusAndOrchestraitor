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
    public class ConfirmPaymentHandler: ICommandHandler<ConfirmPaymentCommand>
    {
        private readonly IPaymentRepository _repository;

        public ConfirmPaymentHandler(
            IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> HandleAsync(
            ConfirmPaymentCommand command)
        {
            var payment =
                await _repository.GetByIdAsync(command.PaymentId);

            if (payment == null)
            {
                throw new Exception("Payment not found.");
            }

            payment.Confirm();

            await _repository.SaveAsync();

            return new List<Event>
            {
                new PaymentCompletedEvent(
                    payment.PaymentId,
                    payment.OrderId,
                    payment.Amount)
            };
        }
    }
}
