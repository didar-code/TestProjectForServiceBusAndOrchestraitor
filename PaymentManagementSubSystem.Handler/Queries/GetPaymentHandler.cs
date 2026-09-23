using PaymentManagementSubSystem.DTOs.Responses;
using PaymentManagementSubSystem.Handler.Mapping;
using PaymentManagementSubSystem.Repository.Interfaces;
using SharedSubSystem.Redis.Caching;

namespace PaymentManagementSubSystem.Handler.Queries
{
    public class GetPaymentHandler
    {
        private readonly IPaymentRepository _repository;
        private readonly IRedisCacheService _cache;

        public GetPaymentHandler(
            IPaymentRepository repository,
            IRedisCacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetAllAsync()
        {
            var payments =
                await _repository.GetAllAsync();

            return payments.Select(
                x => x.ToResponseDto());
        }

        public async Task<PaymentResponseDto?> GetByIdAsync(
            int paymentId)
        {
            var cacheKey =
                $"payment:{paymentId}";

            var cached =
                await _cache.GetAsync<PaymentResponseDto>(
                    cacheKey);

            if (cached != null)
            {
                return cached;
            }

            var payment =
                await _repository.GetByIdAsync(
                    paymentId);

            if (payment == null)
            {
                throw new Exception(
                    "Payment not found.");
            }

            var result =
                payment.ToResponseDto();

            await _cache.SetAsync(
                cacheKey,
                result,
                TimeSpan.FromMinutes(10));

            return result;
        }
    }
}