namespace Domain.Entities.OrderAggregate;

public class OrderItem {

	public int Id { get; protected set; }

	// The order to which this item belongs
	public int OrderId { get; init; }

	public int Qty { get; private set; }

	public CatalogItemOrdered OrderedItem { get; init; }

	public OrderItem(int orderId, CatalogItemOrdered item) {
		OrderId = orderId;
		OrderedItem = item;
	}

	public void SetQty(int qty) {
		if (qty < 0) throw new ArgumentOutOfRangeException(nameof(qty));
		Qty = qty;
	}


}