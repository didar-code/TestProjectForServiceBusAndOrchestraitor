using SharedSubSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Aggregators
{
    public class PaymentAggregatorsRoot
    {
        public int PaymentId { get; private set; }

        public int OrderId { get; private set; }

        public decimal Amount { get; private set; }

        public string PaymentMethod { get; private set; } = string.Empty;

        public string Status { get; private set; } = string.Empty;

        public DateTime PaymentDate { get; private set; }

        private PaymentAggregatorsRoot()
        {
        }

        public static PaymentAggregatorsRoot Create(int orderId, decimal amount, string paymentMethod)
        {
            if (orderId <= 0)
                throw new BusinessRuleException("Order id must be greater than zero.");   

            if (amount <= 0)
                throw new BusinessRuleException("Payment amount must be greater than zero.");  

            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new BusinessRuleException("Payment method is required.");   

            return new PaymentAggregatorsRoot
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Status = "Pending",
                PaymentDate = DateTime.UtcNow
            };
        }

        public PaymentAggregatorsRoot Update(decimal amount, string paymentMethod)
        {
            if (Status == "Confirmed")
                throw new ConflictException("Confirmed payment cannot be updated.");   

            if (Status == "Failed")
                throw new ConflictException("Failed payment cannot be updated.");   

            if (amount <= 0)
                throw new BusinessRuleException("Payment amount must be greater than zero.");  

            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new BusinessRuleException("Payment method is required."); 

            Amount = amount;
            PaymentMethod = paymentMethod;

            return this;
        }

        public void Confirm()
        {
            if (Status == "Confirmed")
                throw new ConflictException("Payment is already confirmed.");   

            if (Status == "Failed")
                throw new ConflictException("Failed payment cannot be confirmed.");  

            Status = "Confirmed";
        }

        public void Fail()
        {
            if (Status == "Confirmed")
                throw new ConflictException("Confirmed payment cannot be failed.");  

            if (Status == "Failed")
                throw new ConflictException("Payment is already failed.");   

            Status = "Failed";
        }
    }
}
