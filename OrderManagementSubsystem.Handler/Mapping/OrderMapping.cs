using OrderManagementSubsystem.Aggregators;
using OrderManagementSubsystem.DTOs.Responses;
using OrderManagementSubsystem.Repository.Repositories;
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
                OrderDate = order.OrderDate,

                
                Items = order.Items.Select(x => new OrderItemResponseDto
                {
                    OrderItemId = x.OrderItemId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    LineTotal = x.LineTotal
                }).ToList()
            };
        }
    }
}
