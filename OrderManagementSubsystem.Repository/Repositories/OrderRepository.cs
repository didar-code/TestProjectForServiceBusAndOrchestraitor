using Microsoft.EntityFrameworkCore;
using OrderManagementSubsystem.Aggregators;
using OrderManagementSubsystem.Repository.Data;
using OrderManagementSubsystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            OrderAggregatorsRoot order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<OrderAggregatorsRoot?> GetByIdAsync(int orderId)
        {
            return await _context.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
        public async Task<IEnumerable<OrderAggregatorsRoot>> SearchAsync(int? orderId,string? customerName,string? status)
        {
            var query = _context.Orders.Include(x => x.Items).AsQueryable();

            if (orderId.HasValue)
            {
                query = query.Where(x =>
                    x.OrderId == orderId.Value);
            }

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                query = query.Where(x =>
                    x.CustomerName.Contains(customerName));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(x =>
                    x.Status == status);
            }

            return await query.ToListAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
