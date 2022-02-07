namespace OrderManager.ApplicationCore.Domain;

public class Order {
    public int Id { get; set; }
    public string Number { get; set; } = default!;
    public string Name { get; set; } = default!;

    public int SupplierId { get; set; }
    public Company? Supplier { get; set; }
    
    public int VendorId { get; set; }
    public Company? Vendor { get; set; }
    
    public int CustomerId { get; set; }
    public Company? Customer { get; set; }
    
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    
    public int PriorityId { get; set; }
    public Priority? Priority { get; set; }

    public string Notes { get; set; } = default!;
};
