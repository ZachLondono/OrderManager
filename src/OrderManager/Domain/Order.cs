namespace OrderManager.ApplicationCore.Domain;

public enum OrderStatus {
    Pending,
    Released,
    Completed,
    Shipped
}

public partial class Order {

    public int OrderId { get; set; }

    public string RefNum { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.Today;

    public List<LineItem> LineItems { get; set; } = new();

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}
