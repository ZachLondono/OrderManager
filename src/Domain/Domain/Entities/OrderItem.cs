namespace Domain.Entities;

public class OrderItem {

	public int Id { get; protected set; }

	// The order to which this item belongs
	public int OrderId { get; init; }

	public int ProductId => ProductOrdered.Id;
	// The product which this item contains
	public CatalogProduct ProductOrdered { get; init; }

	public int Qty { get; private set; }

	// The options and their specific values in this item
	private readonly Dictionary<string, string> _options = new();
	public IReadOnlyDictionary<string, string> Options => _options;

	public OrderItem(int orderId, CatalogProduct productOrdered) {
		OrderId = orderId;
		ProductOrdered = productOrdered;
		foreach (string option in ProductOrdered.Attributes)
			_options.Add(option, string.Empty);
	}

	public void SetQty(int qty) {
		if (qty < 0) throw new ArgumentOutOfRangeException(nameof(qty));
		Qty = qty;
	}

	public void SetOptionValue(string option, string value) {
		if (!_options.ContainsKey(option)) return;
		_options[option] = value;
	}

}