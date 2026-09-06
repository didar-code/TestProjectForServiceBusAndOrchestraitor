using PaymentManagementSubSystem.Aggregators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(PaymentAggregatorsRoot payment);

        Task<PaymentAggregatorsRoot> GetByIdAsync(int paymentId);
        Task<IEnumerable<PaymentAggregatorsRoot>> GetAllAsync();
        Task SaveAsync();
    }
}
