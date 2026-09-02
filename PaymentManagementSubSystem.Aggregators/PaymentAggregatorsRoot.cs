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

        public static PaymentAggregatorsRoot Create(int orderId,decimal amount,string paymentMethod)
        {
            if (orderId <= 0)
                throw new Exception("Order id must be greater than zero.");

            if (amount <= 0)
                throw new Exception("Payment amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new Exception("Payment method is required.");

            return new PaymentAggregatorsRoot
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Status = "Pending",
                PaymentDate = DateTime.UtcNow
            };
        }

        public void Confirm()
        {
            if (Status == "Confirmed")
                throw new Exception("Payment is already confirmed.");

            if (Status == "Failed")
                throw new Exception("Failed payment cannot be confirmed.");

            Status = "Confirmed";
        }

        public void Fail()
        {
            if (Status == "Confirmed")
                throw new Exception("Confirmed payment cannot be failed.");

            if (Status == "Failed")
                throw new Exception("Payment is already failed.");

            Status = "Failed";
        }
    }
}
