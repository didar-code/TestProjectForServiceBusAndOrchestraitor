using PaymentManagementSubSystem.DTOs.Responses;
using PaymentManagementSubSystem.Handler.Mapping;
using PaymentManagementSubSystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Queries
{
    public class GetPaymentHandler
    {
        private readonly IPaymentRepository _repository;

        public GetPaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetAllAsync()
        {
            var payments = await _repository.GetAllAsync();

            return payments.Select(x => x.ToResponseDto());
        }

        public async Task<PaymentResponseDto?> GetByIdAsync(int paymentId)
        {
            var payment = await _repository.GetByIdAsync(paymentId);

            if (payment == null)
                throw new Exception("Payment not found.");

            return payment.ToResponseDto();
        }
    }
}
