namespace Sales.Contracts;

public class OrderedItemDetails {
    
    public Guid ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int LineItem { get; set; }

    public Dictionary<string, string> Options { get; set; } = new();

}
