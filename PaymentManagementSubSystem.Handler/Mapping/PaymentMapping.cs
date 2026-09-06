using PaymentManagementSubSystem.Aggregators;
using PaymentManagementSubSystem.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Mapping
{
    public static class PaymentMapping
    {
        public static PaymentResponseDto ToResponseDto(this PaymentAggregatorsRoot payment)
        {
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
