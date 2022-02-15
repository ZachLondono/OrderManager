namespace Domain.Entities.OrderAggregate;

public class CatalogItemOrdered {

	public int ProductId { get; init; }

	public string ProductName { get; init; }

	// The options and their specific values in this item
	private readonly Dictionary<string, string> _options = new();
	public IReadOnlyDictionary<string, string> Options => _options;

	/// <summary>
	/// Represents a snapshot of an item which was ordered. If the details of a product in the catalog changes, the order items that where part of a completed order should not change
	/// </summary>
	public CatalogItemOrdered(int productId, string productName, IReadOnlyCollection<string> productAttributes) {
		ProductId = productId;
		ProductName = productName;
		foreach (string option in productAttributes)
			_options.Add(option, string.Empty);
	}

	public void SetOptionValue(string option, string value) {
		if (!_options.ContainsKey(option)) return;
		_options[option] = value;
	}

	public bool ContainsOption(string option) {
		return _options.ContainsKey(option);
    }

}