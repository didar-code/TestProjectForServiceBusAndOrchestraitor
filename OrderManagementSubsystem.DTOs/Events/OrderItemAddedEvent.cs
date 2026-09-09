using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.DTOs.Events
{
    public class OrderItemAddedEvent : Event
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NewTotalAmount { get; set; }
    }
}
