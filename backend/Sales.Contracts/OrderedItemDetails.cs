namespace Sales.Contracts;

public class OrderedItemDetails {
    
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ProductClass { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Qty { get; set; }

    public string Options { get; set; } = string.Empty;

}