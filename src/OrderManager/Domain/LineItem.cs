namespace OrderManager.ApplicationCore.Domain;

public class LineItem {

    public int Line { get; set; }

    public int ProductId { get; set; }

    public IReadOnlyDictionary<string, string>? Attributes { get; set; }

}