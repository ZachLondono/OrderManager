using DesktopUI.Models;
using OrderManager.ApplicationCore.Orders;
using OrderManager.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels;

public class OrderListViewModel : ViewModelBase {

    public ObservableCollection<OrderSummary> Orders { get; } = new();

    public List<OrderFilter> OrderFilters { get; }

    private readonly IOrderAPI _api;

    public OrderListViewModel(IOrderAPI api) {
        _api = api;

        OrderFilters = new() {
            new() {
                Name = "All",
                IsChecked = true,
            },
            new() {
                Name = "Pending",
                IsChecked = false,
            },
            new() {
                Name = "Complete",
                IsChecked = false,
            },
            new() {
                Name = "Shipped",
                IsChecked = false,
            }
        };

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            PlacedDate = DateTime.Today,
            CustomerId = 1
        });

    }

    public async Task LoadData() {
        var orders = await _api.GetOrders();
        Orders.Clear();
        foreach (var order in orders) {
            Orders.Add(order);
        }
    }

}
