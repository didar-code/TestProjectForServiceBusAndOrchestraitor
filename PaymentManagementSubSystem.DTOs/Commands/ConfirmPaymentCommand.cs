using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedSubSystem;

namespace PaymentManagementSubSystem.DTOs.Commands
{
    public class ConfirmPaymentCommand : ICommand
    {
        public int PaymentId { get; set; }

        public ConfirmPaymentCommand(int paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
