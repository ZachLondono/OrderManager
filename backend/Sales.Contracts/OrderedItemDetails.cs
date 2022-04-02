namespace Sales.Contracts;

public class OrderedItemDetails {
    
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string Options { get; set; } = string.Empty;

}
