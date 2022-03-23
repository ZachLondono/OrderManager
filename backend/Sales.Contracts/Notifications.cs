using MediatR;

namespace Sales.Contracts;

public record OrderConfirmedNotification(ConfirmedOrder Order) : INotification;

public class ConfirmedOrder {

    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Number { get; set; } = string.Empty;
    
    public string Customer { get; set; } = string.Empty;
    
    public string Vendor { get; set; } = string.Empty;

    public int ItemCount { get; set; }

}

public record OrderVoidNotification(Guid OrderId) : INotification;