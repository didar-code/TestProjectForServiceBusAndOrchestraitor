using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.DTOs.Events
{
    public class OrderCreatedEvent : Event
    {
        public int OrderId { get; set; }
    }
}
