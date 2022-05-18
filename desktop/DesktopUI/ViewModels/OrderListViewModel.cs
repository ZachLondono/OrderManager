using DesktopUI.Models;
using OrderManager.ApplicationCore.Orders;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels;

enum OrderSort {
    Date = 1,
    Priority = 2,
    Name = 3,
    Number = 4
}

public class OrderListViewModel : ViewModelBase {

    public ObservableCollection<OrderListItem> Orders { get; } = new();

    public List<OrderFilter> OrderFilters { get; }

    private int _selectedSort;
    public int SelectedSort {
        get => _selectedSort;
        set => this.RaiseAndSetIfChanged(ref _selectedSort, value);
    }

    private readonly IOrderAPI _api;

    public OrderListViewModel(IOrderAPI api) {
        _api = api;

        SelectedSort = (int) OrderSort.Date;

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
            LastModified = DateTime.Today,
            Customer = new() { Name = "Company A" },
            Vendor = new() { Name = "Company B" },
            Supplier = new() { Name = "Company C" }
        });

        Orders.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            LastModified = DateTime.Today,
            Customer = new() { Name = "Company A" },
            Vendor = new() { Name = "Company B" },
            Supplier = new() { Name = "Company C" }
        });

        Orders.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            LastModified = DateTime.Today,
            Customer = new() { Name = "Company A" },
            Vendor = new() { Name = "Company B" },
            Supplier = new() { Name = "Company C" }
        });

    }

    public async Task LoadData() {
        await Task.CompletedTask;
        /*var orders = await _api.GetOrders();
        Orders.Clear();
        foreach (var order in orders) {
            Orders.Add(order);
        }*/
    }

}
