using OrderManager.Features.OrderDetails.FilledDetails.CompanyDisplay;
using System;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public class OrderDetails {

    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public CompanyDisplayViewModel? Customer { get; set; }

    public CompanyDisplayViewModel? Vendor { get; set; }

    public CompanyDisplayViewModel? Supplier { get; set; }

    public bool IsPriority { get; set; }

    public DateTime LastModified { get; set; }

    public CompanyDisplayViewModel? CompanyModel { get; set; } = new();

}
