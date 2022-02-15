using Domain.Entities.ValueObjects;

namespace Domain.Entities.OrderAggregate;

public class OrderItem {

	public int Id { get; protected set; }

	/// <summary>
	///  The order to which this item belongs
	/// </summary>
	public int OrderId { get; init; }

	/// <summary>
	/// Amount ordered
	/// </summary>
	public Quantity Qty { get; private set; }

	/// <summary>
	/// Item ordered
	/// </summary>
	public CatalogItemOrdered OrderedItem { get; init; }

	/// <summary>
	/// Price of one unit
	/// </summary>
	// public Price UnitPrice { get; init; }

	public OrderItem(int orderId, CatalogItemOrdered item, int qty = 1) {
		OrderId = orderId;
		OrderedItem = item;
		Qty = new(qty);
	}

	public void SetQty(int qty) {
		if (qty < 0) throw new ArgumentOutOfRangeException(nameof(qty));
		Qty = new(qty);
	}


}