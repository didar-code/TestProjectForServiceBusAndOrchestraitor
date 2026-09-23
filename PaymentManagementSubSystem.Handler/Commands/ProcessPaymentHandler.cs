using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.DTOs.Events;
using PaymentManagementSubSystem.Repository.Interfaces;
using SharedSubSystem.Events;
using SharedSubSystem.Exceptions;
using SharedSubSystem.Generics;
using SharedSubSystem.Redis.Caching;
using SharedSubSystem.Redis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Commands
{
    public class ProcessPaymentHandler : ICommandHandler<ProcessPaymentCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IRedisCacheService _cacheService;

        public ProcessPaymentHandler(
            IPaymentRepository repository,
            IRedisCacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Event>> HandleAsync(ProcessPaymentCommand command)
        {
            var payment = await _repository.GetByIdAsync(
                    command.PaymentId);

            if (payment == null)
                throw new NotFoundException("Payment not found.");

            payment.Confirm();

            await _repository.SaveAsync();
            await _cacheService.RemoveAsync(RedisKeyGenerator.Payment(payment.PaymentId));

            return new List<Event>
            {
                new PaymentCompletedEvent(payment.PaymentId,payment.OrderId,payment.Amount)
            };
        }
    }
}
