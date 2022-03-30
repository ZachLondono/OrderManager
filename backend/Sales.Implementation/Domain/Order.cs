namespace Sales.Implementation.Domain;

public enum OrderStatus {
    Bid,
    Confirmed,
    Completed,
    Void
}

public class Order {

    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public string Number { get; set; }

    public Company? Customer { get; set; }
    
    public Company? Vendor { get; set; }
    
    public Company? Supplier { get; set; }
    
    public OrderStatus Status { get; set; }

    private readonly List<OrderedItem> _items;
    public IReadOnlyCollection<OrderedItem> Items => _items;
    
    public Dictionary<string, string> Fields { get; set; } = new();
    
    public DateTime? CompletionDate { get; set; }

    public DateTime? ConfirmationDate { get; set; }
    
    public DateTime? PlacedDate { get; set; }

    public Order(Guid id, string name, string number, DateTime? placedDate) {
        Id = id;
        Name = name;
        Number = number;
        PlacedDate = placedDate;
        _items = new List<OrderedItem>();
        Status = OrderStatus.Bid;
    }

    public void AddItem(OrderedItem item) {
        if (item is null)
            throw new ArgumentNullException(nameof(item));
        _items.Add(item);
    }

    public void RemoveItem(OrderedItem item) {
        _items.Remove(item);
    }
    
    public void RemoveItem(Guid itemId) {
        var idx = _items.FindIndex(i => i.Id.Equals(itemId));
        _items.RemoveAt(idx);
    }

    public void ConfirmOrder() {
        ConfirmationDate = DateTime.Now;
        Status = OrderStatus.Confirmed;
    }

    public void CompleteOrder() {
        if (ConfirmationDate is null)
            ConfirmationDate = DateTime.Now;
        CompletionDate = DateTime.Now;
        Status = OrderStatus.Completed;
    }

    public void VoidOrder() {
        Status = OrderStatus.Void;
    }

}