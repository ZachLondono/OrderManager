using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManager.Features.OrderDetails;

public class OrderDetails {

    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public CompanyDetails? Customer { get; set; }

    public int VendorId { get; set; }
    public CompanyDetails? Vendor { get; set; }

    public int SupplierId { get; set; }
    public CompanyDetails? Supplier { get; set; }

    public bool IsPriority { get; set; }

    public DateTime LastModified { get; set; }

    public IEnumerable<OrderedProduct> OrderedProducts { get; set; } = Enumerable.Empty<OrderedProduct>();

}

public class CompanyDetails {

    public string Name { get; set; } = string.Empty;

    public string Contact { get; set; } = string.Empty;

    public string Address1 { get; set; } = string.Empty;

    public string Address2 { get; set; } = string.Empty;

    public string Address3 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

}

public class OrderedProduct {

    /// <summary>
    /// Id of the product configuration in the database
    /// </summary>
    public int Id { get; set; }

    public int Qty { get; set; }

    /// <summary>
    /// Id of the product being ordered
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Name of the product at the time it was ordered
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Configuration of the product
    /// </summary>
    public IEnumerable<ProductOption> Options { get; set; } = Enumerable.Empty<ProductOption>();

}

public class ProductOption {

    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

}