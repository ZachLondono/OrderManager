using Avalonia.Controls;
using Domain.Entities.OrderAggregate;
using OrderManager.Features.OrderDetails.FilledDetails.CompanyDisplay;
using System;
using System.Collections.Generic;

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

    public IReadOnlyDictionary<int, OrderedProductViewModel> Products { get; set; } = new Dictionary<int, OrderedProductViewModel>();
    
}

public class OrderedProductViewModel {

    public List<OrderItem> Items { get; set; } = new();

}