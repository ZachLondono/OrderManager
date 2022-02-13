namespace Domain.Entities;

public class Order {

	public int Id { get; set; }

	public string Number { get; set; } = string.Empty;

	private readonly List<OrderItem> _items = new();

	public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

	public void AddItem(CatalogProduct catalogProduct, int qty) {
		OrderItem item = new(Id, catalogProduct);
		item.SetQty(qty);
		_items.Add(item);
	}

}
