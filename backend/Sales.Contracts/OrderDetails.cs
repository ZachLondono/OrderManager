namespace Sales.Contracts;

public class OrderDetails {

    public Guid Id {  get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public string Vendor { get; set; } = string.Empty;

    public string Supplier { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime ConfirmationDate { get; set; }

    public Dictionary<string, string> OrderFields { get; set; } = new();

    public OrderedItemDetails[] OrderedItems { get; set; } = new OrderedItemDetails[0];

}
