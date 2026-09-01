using OrderManagementSubsystem.DTOs.Queries;
using OrderManagementSubsystem.DTOs.Responses;
using OrderManagementSubsystem.Repository.Interfaces;
using SharedSubSystem.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Queries
{
    public class SearchOrderHandler: IQueryHandler<SearchOrderQuery,IEnumerable<OrderResponseDto>>
    {
        private readonly IOrderRepository _repository;

        public SearchOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderResponseDto>> HandleAsync(SearchOrderQuery query)
        {
            var orders = await _repository.SearchAsync(
                query.OrderId,
                query.CustomerName,
                query.Status);

            return orders.Select(x => new OrderResponseDto
            {
                OrderId = x.OrderId,
                CustomerName = x.CustomerName,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                OrderDate = x.OrderDate
            });
        }
    }
}
