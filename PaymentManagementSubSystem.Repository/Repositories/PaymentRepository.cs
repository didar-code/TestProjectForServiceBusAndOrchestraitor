using Microsoft.EntityFrameworkCore;
using PaymentManagementSubSystem.Aggregators;
using PaymentManagementSubSystem.Repository.Data;
using PaymentManagementSubSystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Repository.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PaymentAggregatorsRoot payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<PaymentAggregatorsRoot> GetByIdAsync(
            int paymentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == paymentId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
