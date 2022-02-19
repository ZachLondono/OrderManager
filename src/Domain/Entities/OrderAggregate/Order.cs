namespace Domain.Entities.OrderAggregate;

public class Order {

	public int Id { get; set; }

	public string Number { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public bool IsPriority { get; set; } = false;

	public DateTime LastModified { get; set; } = DateTime.Today;

	public Company? Customer { get; set; }

	public Company? Vendor { get; set; }

	public Company? Supplier { get; set; }

	private readonly List<OrderItem> _items = new();

	public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

	public Order() { }
	public Order(List<OrderItem> items) {
		_items = items;
    }

	public void AddItem(CatalogItemOrdered catalogItem, int qty) {
		OrderItem item = new(Id, catalogItem);
		item.SetQty(qty);
		_items.Add(item);
	}

}
