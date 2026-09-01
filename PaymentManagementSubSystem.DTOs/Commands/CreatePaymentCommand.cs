using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedSubSystem;

namespace PaymentManagementSubSystem.DTOs.Commands
{
    public class CreatePaymentCommand : ICommand
    {
        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }
    }
}
