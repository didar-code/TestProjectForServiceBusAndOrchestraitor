using PaymentManagementSubSystem.DTOs.Responses;
using SharedSubSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.DTOs.Queries
{
    public class GetPaymentByIdQuery : IQuery<PaymentResponseDto>
    {
        public int PaymentId { get; set; }
    }
}
