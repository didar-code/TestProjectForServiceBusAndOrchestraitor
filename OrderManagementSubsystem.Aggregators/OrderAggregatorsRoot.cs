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

        private OrderAggregatorsRoot()
        {
        }

        public static OrderAggregatorsRoot Create(
            string customerName,
            decimal totalAmount)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new Exception("Customer name is required.");

            if (totalAmount <= 0)
                throw new Exception("Total amount must be greater than zero.");

            return new OrderAggregatorsRoot
            {
                CustomerName = customerName,
                TotalAmount = totalAmount,
                Status = "Pending",
                OrderDate = DateTime.UtcNow
            };
        }

        public void Confirm()
        {
            if (Status == "Confirmed")
                throw new Exception("Order is already confirmed.");

            Status = "Confirmed";
        }
    }
}
