using OrderManagementSubsystem.DTOs.Responses;
using SharedSubSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.DTOs.Queries
{
    public class SearchOrderQuery : IQuery<IEnumerable<OrderResponseDto>>
    {
        public int? OrderId { get; set; }

        public string? CustomerName { get; set; }

        public string? Status { get; set; }
    }
}
