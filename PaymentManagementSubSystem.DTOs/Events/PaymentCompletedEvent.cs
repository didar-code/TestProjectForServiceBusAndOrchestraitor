using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.DTOs.Events
{
    public class PaymentCompletedEvent : Event
    {
        public int PaymentId { get; }

        public int OrderId { get; }

        public decimal Amount { get; }

        public PaymentCompletedEvent(int paymentId,int orderId,decimal amount)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            Amount = amount;
        }
    }
}
