namespace Sales.Implementation.Infrastructure.Persistance;

internal class Order {

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid VendorId { get; set; }

    public Guid SupplierId { get; set; }

    public DateTime? PlacedDate { get; set; }

    public DateTime? ConfirmationDate { get; set; }
    
    public DateTime? CompletionDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string OrderFields { get; set; } = string.Empty;

}