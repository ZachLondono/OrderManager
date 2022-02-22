﻿namespace Persistance.Repositories.Orders;

public class OrderDAO {

    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsPriority { get; set; } = false;

    public DateTime LastModified { get; set; } = DateTime.Today;

    public int CustomerId { get; set; }
    
    public int VendorId { get; set; }

    public int SupplierId { get; set; }

}