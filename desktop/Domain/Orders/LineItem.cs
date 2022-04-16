namespace OrderManager.Domain.Orders;

public class LineItem {

    public int Id { get; init; }

    public string ProductName { get; init; }

    public int Qty { get; init; }

    public int LineNumber { get; init; }

    private readonly Dictionary<string, string> _attributes;
    public IReadOnlyDictionary<string, string> Attributes => _attributes;

    public LineItem(int id, string productName, int qty, int lineNumber, Dictionary<string, string> attributes) {
        Id = id;
        ProductName = productName;
        Qty = qty;
        LineNumber = lineNumber;
        _attributes = attributes;
    }

}