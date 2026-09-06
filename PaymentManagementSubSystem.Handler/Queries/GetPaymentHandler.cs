using PaymentManagementSubSystem.DTOs.Responses;
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

            return payments.Select(x => new PaymentResponseDto
            {
                PaymentId = x.PaymentId,
                OrderId = x.OrderId,
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod,
                Status = x.Status,
                PaymentDate = x.PaymentDate
            });
        }

        public async Task<PaymentResponseDto?> GetByIdAsync(int paymentId)
        {
            var payment = await _repository.GetByIdAsync(paymentId);

            if (payment == null)
                return null;

            return new PaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}
