using Domain.Entities.OrderAggregate;
using OrderManager.Shared;
using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public class FilledOrderDetailsViewModel : ViewModelBase {

    public OrderDetails Details { get; set; }
    
    public ReactiveCommand<int, Unit> ReleaseOrder { get; }

    public FilledOrderDetailsViewModel(Order order) {
        
        IReadOnlyDictionary<int, OrderedProductViewModel> productsOrdered = order.Items
                                .GroupBy(oi => oi.OrderedItem.ProductId)
                                .ToDictionary(x => x.Key,
                                                x => new OrderedProductViewModel() {
                                                    ProductName = x.FirstOrDefault()?.OrderedItem.ProductName ?? "Unkown",
                                                    Items = x.ToList()
                                                }
                                );

        Details = new() {
            Id = order.Id,
            Number = order.Number,
            Name = order.Name,
            IsPriority = order.IsPriority,
            LastModified = order.LastModified,
            Customer = new() {
                CompanyRole = "Customer",
                Company = order.Customer
            },
            Vendor = new() {
                CompanyRole = "Vendor",
                Company = order.Vendor
            },
            Supplier = new() {
                CompanyRole = "Supplier",
                Company = order.Supplier
            },
            Products = productsOrdered
        };
        ReleaseOrder = ReactiveCommand.Create<int>(OnOrderRelease);
    }

    public static void OnOrderRelease(int orderId) {
        Debug.WriteLine($"Releasing order {orderId}");
    }

}
