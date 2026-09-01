using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Interfaces
{
    public interface IPaymentCommandHandler
    {
        Task<IEnumerable<Event>> ProcessPaymentAsync(int paymentId);
        Task<IEnumerable<Event>> FailPaymentAsync(int paymentId);
    }
}
