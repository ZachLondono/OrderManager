using Domain.Entities.OrderAggregate;
using MediatR;
using OrderManager.Shared;
using OrderManager.Shared.Messages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public class FilledOrderDetailsViewModel : ViewModelBase {

    public OrderDetails Details { get; set; }
    
    public ReactiveCommand<Guid, Unit> ReleaseOrder { get; }

    public ReactiveCommand<Guid, Unit> RemakeOrder { get; }

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

        ReleaseOrder = ReactiveCommand.Create<Guid>(OnOrderRelease);
        RemakeOrder = ReactiveCommand.Create<Guid>(OnOrderRemake);
    }

    public static void OnOrderRelease(Guid orderId) {
        Debug.WriteLine($"Releasing order {orderId}");
    }

    public static async void OnOrderRemake(Guid orderId) {
        Debug.WriteLine($"Remaking order {orderId}");

        var sender = Program.GetService<IMediator>();
        try {

            Guid newId = await sender.Send(new CreateRemake.Command(orderId, new() {
                new(1, 10) // test values, should open dialog to choose item quantities
            }));

            MessageBus.Current.SendMessage(new OrderUploaded(newId));

        } catch(Exception e) {
            Debug.WriteLine(e);
        }
    }

}
