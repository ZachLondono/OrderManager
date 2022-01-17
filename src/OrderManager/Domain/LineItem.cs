namespace OrderManager.ApplicationCore.Domain;

public class LineItem {

    public int LineItemId { get; set; }

    public int Line { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public IReadOnlyDictionary<string, object>? Attributes { get; set; }

}