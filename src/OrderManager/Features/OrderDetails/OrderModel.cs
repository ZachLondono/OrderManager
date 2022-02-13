using System;

namespace OrderManager.Features.OrderDetails;

public class OrderModel {

    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public int ItemCount { get; set; }

    public DateTime? ReleaseDate { get; set; } = null;

}
