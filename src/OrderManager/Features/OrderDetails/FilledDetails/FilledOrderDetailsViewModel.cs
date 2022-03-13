using MediatR;
using OrderManager.Features.OrderDetails.CompanyDisplay;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public record ProductGroup(string ProductName, List<OrderedProduct> Products);

public class FilledOrderDetailsViewModel : ViewModelBase {

    public OrderDetails Details { get; set; }
    public CompanyDisplayViewModel Customer { get; set; }
    public CompanyDisplayViewModel Vendor { get; set; }
    public CompanyDisplayViewModel Supplier { get; set; }
    public List<ProductGroup> GroupedProducts { get; set; }

    public ReactiveCommand<Guid, Unit> ReleaseOrder { get; }

    public ReactiveCommand<Guid, Unit> RemakeOrder { get; }

    private ApplicationContext _context;

    public FilledOrderDetailsViewModel(ApplicationContext context, OrderDetails order) {

        _context = context;
        Details = order;

        GroupedProducts = Details.OrderedProducts
                                    .GroupBy(p => p.ProductName)
                                    .Select(g => new ProductGroup(g.Key, g.ToList()))
                                    .ToList();

        Customer = new() { Company = Details.Customer };
        Vendor = new() { Company = Details.Vendor };
        Supplier = new() { Company = Details.Supplier };

        ReleaseOrder = ReactiveCommand.Create<Guid>(OnOrderRelease);
        RemakeOrder = ReactiveCommand.Create<Guid>(OnOrderRemake);
    }

    public static void OnOrderRelease(Guid orderId) {
        Debug.WriteLine($"Releasing order {orderId}");
    }

    public async void OnOrderRemake(Guid orderId) {
        Debug.WriteLine($"Remaking order {orderId}");

        var sender = Program.GetService<IMediator>();
        try {

            Guid newId = await sender.Send(new CreateRemake.Command(orderId, new() {
                new(1, 10) // TODO: open dialog to choose quantities of order to remake
            }));

            _context.AddOrder(newId);

        } catch(Exception e) {
            Debug.WriteLine(e);
        }
    }

}
