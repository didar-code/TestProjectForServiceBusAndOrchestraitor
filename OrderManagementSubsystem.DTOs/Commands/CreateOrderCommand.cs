using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedSubSystem;

namespace OrderManagementSubsystem.DTOs.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public string CustomerName { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
