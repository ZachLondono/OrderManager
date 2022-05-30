using MediatR;

namespace Sales.Contracts;

public record OrderReleasedNotification(ReleasedOrder Order) : INotification;

public class ReleasedOrder {

    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public IEnumerable<ProductOrdered> Products { get; set; } = Enumerable.Empty<ProductOrdered>();

}

public class ProductOrdered {

    public int ProductId { get; set; }

    public int ProductClass { get; set; }

    public int QtyOrdered { get; set; }

}

public record OrderVoidNotification(int OrderId) : INotification;