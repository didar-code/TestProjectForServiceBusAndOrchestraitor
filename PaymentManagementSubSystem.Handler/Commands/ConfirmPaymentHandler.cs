using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Events;
using PaymentManagementSubSystem.Repository.Interfaces;
using SharedSubSystem.Events;
using SharedSubSystem.Generics;
using SharedSubSystem.Redis.Caching;

namespace PaymentManagementSubSystem.Handler.Commands
{
    public class ConfirmPaymentHandler
        : ICommandHandler<ConfirmPaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IRedisCacheService _cache;

        public ConfirmPaymentHandler(
            IPaymentRepository repository,
            IRedisCacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<IEnumerable<Event>> HandleAsync(ConfirmPaymentCommand command)
        {
            var payment =
                await _repository.GetByIdAsync(command.PaymentId);

            if (payment == null)
            {
                throw new Exception("Payment not found.");
            }

            payment.Confirm();

            await _repository.SaveAsync();

            await _cache.RemoveAsync($"payment:{command.PaymentId}");

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