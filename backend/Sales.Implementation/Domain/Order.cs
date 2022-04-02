namespace Sales.Implementation.Domain;

public enum OrderStatus {
    Bid,
    Confirmed,
    Completed,
    Void
}

public class Order {

    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Number { get; set; }

    public int CustomerId { get; set; }
    
    public int VendorId { get; set; }
    
    public int SupplierId { get; set; }
    
    public OrderStatus Status { get; set; }

    public int[] Items;
    
    public Dictionary<string, string> Fields { get; set; } = new();
    
    public DateTime? CompletionDate { get; set; }

    public DateTime? ConfirmationDate { get; set; }
    
    public DateTime? PlacedDate { get; set; }

    public Order(int id, string name, string number, int[] items, OrderStatus status, DateTime? placedDate) {
        Id = id;
        Name = name;
        Number = number;
        Items = items;
        Status = status;
        PlacedDate = placedDate;
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