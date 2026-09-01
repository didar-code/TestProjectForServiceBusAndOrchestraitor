using OrderManagementSubsystem.Aggregators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderAggregatorsRoot order);

        Task<OrderAggregatorsRoot?> GetByIdAsync(int orderId);
        Task<IEnumerable<OrderAggregatorsRoot>> SearchAsync(int? orderId,string? customerName,string? status);

        Task SaveAsync();
    }
}
