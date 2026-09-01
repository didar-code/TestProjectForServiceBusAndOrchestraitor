using OrderManagementSubsystem.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedSubSystem;

namespace OrderManagementSubsystem.DTOs.Commands
{
    public class ConfirmOrderCommand : ICommand
    {
        public int OrderId { get; set; }

        public int PaymentId { get; set; }
    }
}
