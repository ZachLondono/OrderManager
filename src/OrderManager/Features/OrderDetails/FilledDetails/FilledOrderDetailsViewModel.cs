using Domain.Entities.OrderAggregate;
using OrderManager.Shared;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public class FilledOrderDetailsViewModel : ViewModelBase {

    public OrderDetails Details { get; set; }
    
    public ReactiveCommand<int, Unit> ReleaseOrder { get; }

    public FilledOrderDetailsViewModel(Order order) {
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
            }
        };
        ReleaseOrder = ReactiveCommand.Create<int>(OnOrderRelease);
    }

    public void OnOrderRelease(int orderId) {
        Debug.WriteLine($"Releasing order {orderId}");
    }

}
