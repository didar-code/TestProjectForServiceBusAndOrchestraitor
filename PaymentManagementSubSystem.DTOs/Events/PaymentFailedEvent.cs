using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.DTOs.Events
{
    public class PaymentFailedEvent : Event
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }

        public PaymentFailedEvent(int paymentId,int orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
        }
    }
}
