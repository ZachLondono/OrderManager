namespace Sales.Implementation.Infrastructure.Persistance;

public class OrderedItem {

    public Guid Id { get; init; }

    public string ProductName { get; init; }

    public Guid ProductId { get; init; }

    public int Qty { get; init; }

    public string Options { get; init; }

    public Guid OrderId { get; init; }

    public OrderedItem(Guid id, string productName, Guid productId, int qty, string options, Guid orderId) {
        Id = id;
        ProductName = productName;
        ProductId = productId;
        Qty = qty;
        Options = options;
        OrderId = orderId;
    }

}
