namespace Sales.Implementation.Infrastructure.Persistance;

public class OrderedItem {

    public int Id { get; init; }

    public string ProductName { get; init; }

    public int ProductId { get; init; }

    public int Qty { get; init; }

    public string Options { get; init; }

    public int OrderId { get; init; }

    public OrderedItem(int id, string productName, int productId, int qty, string options, int orderId) {
        Id = id;
        ProductName = productName;
        ProductId = productId;
        Qty = qty;
        Options = options;
        OrderId = orderId;
    }

}
