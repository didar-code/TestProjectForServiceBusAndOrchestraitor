
using SharedSubSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Aggregators
{
    public class OrderAggregatorsRoot
    {
        public int OrderId { get; private set; }

        public string CustomerName { get; private set; }

        public decimal TotalAmount { get; private set; }

        public string Status { get; private set; }

        public DateTime OrderDate { get; private set; }


        private readonly List<OrderItemAggregator> _items = new();

        public IReadOnlyCollection<OrderItemAggregator> Items => _items.AsReadOnly();

        private OrderAggregatorsRoot()
        {
        }

        public static OrderAggregatorsRoot Create(string customerName,
            decimal totalAmount)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new BusinessRuleException("Customer name is required.");   

            if (totalAmount <= 0)
                throw new BusinessRuleException("Total amount must be greater than zero."); 
            return new OrderAggregatorsRoot
            {
                CustomerName = customerName,
                TotalAmount = totalAmount,
                Status = "Pending",
                OrderDate = DateTime.UtcNow
            };
        }


        public OrderAggregatorsRoot Update(string customerName, decimal totalAmount)
        {
            if (Status == "Confirmed")
                throw new ConflictException(   
                    "Confirmed order cannot be updated.");

            if (string.IsNullOrWhiteSpace(customerName))
                throw new BusinessRuleException(   
                    "Customer name is required.");

            if (totalAmount <= 0)
                throw new BusinessRuleException(  
                    "Total amount must be greater than zero.");

            CustomerName = customerName;
            TotalAmount = totalAmount;

            return this;
        }

        public void AddItem(string productName, int quantity, decimal unitPrice)
        {
            if (Status != "Pending")
                throw new ConflictException("Items can only be added while order is pending.");   

            var item = OrderItemAggregator.Create(productName, quantity, unitPrice);

            _items.Add(item);

            TotalAmount = _items.Sum(x => x.LineTotal);
        }

        public void RemoveItem(int orderItemId)
        {
            if (Status != "Pending")
                throw new ConflictException("Items can only be removed while order is pending.");   

            var item = _items.FirstOrDefault(x => x.OrderItemId == orderItemId);

            if (item == null)
                throw new NotFoundException("Order item not found."); 

            _items.Remove(item);

            TotalAmount = _items.Sum(x => x.LineTotal);
        }

        public void Confirm()
        {
            if (Status == "Confirmed")
                throw new ConflictException("Order is already confirmed.");   

            if (!_items.Any())
                throw new BusinessRuleException("Cannot confirm an order with no items.");  

            Status = "Confirmed";
        }
    }
}