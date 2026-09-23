using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Events;
using PaymentManagementSubSystem.Repository.Interfaces;

using SharedSubSystem.Events;
using SharedSubSystem.Generics;
using SharedSubSystem.Redis.Caching;
using SharedSubSystem.Redis.Core;

namespace PaymentManagementSubSystem.Handler.Commands
{
    public class UpdatePaymentHandler
        : ICommandHandler<UpdatePaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IRedisCacheService _cache;

        public UpdatePaymentHandler(
            IPaymentRepository repository,
            IRedisCacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<IEnumerable<Event>> HandleAsync(
            UpdatePaymentCommand command)
        {
            var payment =
                await _repository.GetByIdAsync(
                    command.PaymentId);

            if (payment == null)
                throw new Exception(
                    "Payment not found.");

            payment.Update(
                command.Amount,
                command.PaymentMethod);

            await _repository.SaveAsync();

            await _cache.RemoveAsync(RedisKeyGenerator.Payment(payment.PaymentId));

            return new List<Event>
            {
                new PaymentUpdatedEvent
                {
                    PaymentId = payment.PaymentId,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount
                }
            };
        }
    }
}