namespace Sales.Implementation.Domain;

public class OrderedItem {
    
    public int Id { get; init; }
    
    public int ProductId { get; init; }

    public int ProductClass { get; init; }

    public int OrderId { get; init; }
    
    public int Quantity { get; private set; }

    private readonly Dictionary<string, string> _options = new();

    public IReadOnlyDictionary<string, string> Options => _options;

    public string this[string option] {
        get => Options[option];
        set => SetOption(option, value);
    }

    public OrderedItem(int id, int productId, int productClass, int orderId) {
        Id = id;
        ProductId = productId;
        ProductClass = productClass;
        OrderId = orderId;
    }

    public OrderedItem(int id, int productId, int productClass, int qty, Dictionary<string, string> options) {
        Id = id;
        ProductId = productId;
        ProductClass = productClass;
        SetQuantity(qty);
        _options = options;
    }

    public void SetQuantity(int qty) {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty), "Quantity must be greater 0");
        Quantity = qty;
    }

    public void SetOption(string option, string value) {
        if (string.IsNullOrEmpty(option) || string.IsNullOrWhiteSpace(option))
            throw new ArgumentNullException(nameof(option));
        if (value is null) throw new ArgumentNullException(nameof(option));
        _options[option] = value;
    }

}
