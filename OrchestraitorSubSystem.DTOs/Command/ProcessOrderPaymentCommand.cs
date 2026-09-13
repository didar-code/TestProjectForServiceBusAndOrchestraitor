using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestraitorSubSystem.DTOs.Command
{
    public class ProcessOrderPaymentCommand
    {
        public int OrderId { get; set; }

        public int PaymentId { get; set; }
    }
}
