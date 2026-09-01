using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedSubSystem;

namespace PaymentManagementSubSystem.DTOs.Commands
{
    public class FailPaymentCommand : ICommand
    {
        public int PaymentId { get; set; }
    }
}
