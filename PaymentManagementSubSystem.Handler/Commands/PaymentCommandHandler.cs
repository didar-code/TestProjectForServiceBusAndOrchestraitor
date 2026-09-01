using PaymentManagementSubSystem.DTOs.Events;
using PaymentManagementSubSystem.Handler.Interfaces;
using PaymentManagementSubSystem.Repository.Interfaces;
using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Commands
{
    public class PaymentCommandHandler : IPaymentCommandHandler
    {
        private readonly IPaymentRepository _repository;

        public PaymentCommandHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> ProcessPaymentAsync(
            int paymentId)
        {
            var payment =
                await _repository.GetByIdAsync(paymentId);

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

        public async Task<IEnumerable<Event>> FailPaymentAsync(
            int paymentId)
        {
            var payment =
                await _repository.GetByIdAsync(paymentId);

            if (payment == null)
            {
                throw new Exception("Payment not found.");
            }

           
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
