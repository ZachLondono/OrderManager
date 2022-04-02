using MediatR;

namespace Sales.Contracts;

public record OrderConfirmedNotification(ConfirmedOrder Order) : INotification;

public class ConfirmedOrder {

    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Number { get; set; } = string.Empty;
    
    public int CustomerId { get; set; }
    
    public int VendorId { get; set; }

    public int ItemCount { get; set; }

}

public record OrderVoidNotification(int OrderId) : INotification;