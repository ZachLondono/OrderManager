namespace Sales.Implementation.Domain;

public enum OrderStatus {
    Bid,
    Confirmed,
    Completed,
    Void
}

public class Order {

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Number { get; set; } = string.Empty;

    public Company? Customer { get; set; }
    
    public Company? Vendor { get; set; }
    
    public Company? Supplier { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public List<OrderedItem> Items { get; set; } = new();
    
    public Dictionary<string, string> Fields { get; set; } = new();
    
    public DateTime ConfirmationDate { get; set; }
    
    public DateTime PlacedDate { get; set; }

    public Order() {

    }

    public void AddItem(OrderedItem item) => throw new NotImplementedException();
    
    public void RemoveItem(OrderedItem item) => throw new NotImplementedException();
    
    public void PlaceOrder() => throw new NotImplementedException();

    public void VoidOrder() => throw new NotImplementedException();

}