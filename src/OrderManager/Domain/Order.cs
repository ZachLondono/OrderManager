namespace OrderManager.ApplicationCore.Domain;

public partial class Order {

    public int OrderId { get; set; }

    public string RefNum { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.Today;

    public List<LineItem> LineItems { get; set; } = new();

}
