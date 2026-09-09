using OrderManagementSubsystem.Aggregators;

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


    public OrderAggregatorsRoot Update(string customerName, decimal totalAmount)
    {
        if (Status == "Confirmed")
            throw new Exception(
                "Confirmed order cannot be updated.");

        if (string.IsNullOrWhiteSpace(customerName))
            throw new Exception(
                "Customer name is required.");

        if (totalAmount <= 0)
            throw new Exception(
                "Total amount must be greater than zero.");

        CustomerName = customerName;
        TotalAmount = totalAmount;

        return this;
    }

    public void AddItem(string productName, int quantity, decimal unitPrice)
    {
        if (Status != "Pending")
            throw new Exception("Items can only be added while order is pending.");

        var item = OrderItemAggregator.Create(productName, quantity, unitPrice);

        _items.Add(item);

        TotalAmount = _items.Sum(x => x.LineTotal);
    }

    public void RemoveItem(int orderItemId)
    {
        if (Status != "Pending")
            throw new Exception("Items can only be removed while order is pending.");

        var item = _items.FirstOrDefault(x => x.OrderItemId == orderItemId);

        if (item == null)
            throw new Exception("Order item not found.");

        _items.Remove(item);

        TotalAmount = _items.Sum(x => x.LineTotal);
    }

    public void Confirm()
    {
        if (Status == "Confirmed")
            throw new Exception("Order is already confirmed.");

        if (!_items.Any())
            throw new Exception("Cannot confirm an order with no items.");

        Status = "Confirmed";
    }
}