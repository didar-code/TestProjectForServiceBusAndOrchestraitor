using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Interfaces
{
    public interface IOrderCommandHandler
    {
        Task<IEnumerable<Event>> ConfirmOrderAsync(int orderId,int paymentId);
    }
}
