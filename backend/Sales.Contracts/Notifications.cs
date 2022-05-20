using MediatR;

namespace Sales.Contracts;

public record OrderConfirmedNotification(ConfirmedOrder Order) : INotification;

public class ConfirmedOrder {

    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public List<ProductOrdered> Products { get; set; } = new();

}

public class ProductOrdered {

    public int ProductId { get; set; }

    public int QtyOrdered { get; set; }

}

public record OrderVoidNotification(int OrderId) : INotification;