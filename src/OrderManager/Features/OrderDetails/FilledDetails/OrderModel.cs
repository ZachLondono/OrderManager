using System;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public class OrderDetails {

    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsPriority { get; set; }

    public DateTime LastModified { get; set; }

}
