using SharedSubSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.DTOs.Commands
{
    public class ProcessPaymentCommand : ICommand
    {
        public int PaymentId { get; set; }

        public ProcessPaymentCommand(int paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
