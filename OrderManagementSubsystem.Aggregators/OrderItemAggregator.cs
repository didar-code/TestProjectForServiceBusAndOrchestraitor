using SharedSubSystem;
using SharedSubSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Aggregators
{
    public class OrderItemAggregator:IAggregator
    {
        public int OrderItemId { get; private set; }

        public int OrderId { get; private set; }

        public string ProductName { get; private set; } = string.Empty;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal LineTotal => Quantity * UnitPrice;

        private OrderItemAggregator()
        {
        }


         internal static OrderItemAggregator Create(string productName, int quantity, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new BusinessRuleException("Product name is required.");   

            if (quantity <= 0)
                throw new BusinessRuleException("Quantity must be greater than zero.");   

            if (unitPrice <= 0)
                throw new BusinessRuleException("Unit price must be greater than zero.");  

            return new OrderItemAggregator
            {
                ProductName = productName,
                Quantity = quantity,
                UnitPrice = unitPrice
            };
        }
    }
}
