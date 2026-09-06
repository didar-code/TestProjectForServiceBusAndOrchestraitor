using OrderManagementSubsystem.Aggregators;
using OrderManagementSubsystem.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Mapping
{
    public static class OrderMapping
    {
        public static OrderResponseDto ToResponseDto(this OrderAggregatorsRoot order)
        {
            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate
            };
        }
    }
}
