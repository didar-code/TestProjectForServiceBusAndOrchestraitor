using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.DTOs.Events
{
    public class OrderConfirmedEvent : Event
    {
        public int OrderId { get; set; }

        public int PaymentId { get; set; }

        public OrderConfirmedEvent(int orderId,int paymentId)
        {
            OrderId = orderId;
            PaymentId = paymentId;
        }

    }
}
