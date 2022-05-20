namespace Manufacturing.Implementation.Domain;

public class ManufacturedProduct {

    public int Id { get; init; }

    public int ProductId { get; init; }

    public int QtyOrdered { get; init; }

    public ManufacturedProduct(int id, int productId, int qtyOrdered) {
        Id = id;
        ProductId = productId;
        QtyOrdered = qtyOrdered;
    }

}
