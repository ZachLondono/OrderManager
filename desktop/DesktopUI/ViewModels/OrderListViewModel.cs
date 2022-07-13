using DesktopUI.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class OrderListViewModel : ViewModelBase {

    enum OrderSort {
        Date = 1,
        Priority = 2,
        Name = 3,
        Number = 4
    }

    public ObservableCollection<OrderListItem> OrderList { get; } = new();

    public List<OrderFilter> OrderFilters { get; }
    private int _selectedSort;
    public int SelectedSort {
        get => _selectedSort;
        set => this.RaiseAndSetIfChanged(ref _selectedSort, value);
    }

    public ICommand OnSelectedOrder { get; }

    private int _selectedOrderId = -1;
    public int SelectedOrderId {
        get => _selectedOrderId;
        set => this.RaiseAndSetIfChanged(ref _selectedOrderId, value);
    }


    public OrderListViewModel() {

        SelectedSort = (int)OrderSort.Date;

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

        OrderList.Add(new() {
            Id = 1,
            Number = "OT000",
            Name = "Test Order",
            LastModified = DateTime.Today,
            Customer = new() { Name = "Company A" },
            Vendor = new() { Name = "Company B" },
            Supplier = new() { Name = "Company C" }
        });

        OrderList.Add(new() {
            Id = 2,
            Number = "OT001",
            Name = "Another Order",
            LastModified = DateTime.Today,
            Customer = new() { Name = "Company A" },
            Vendor = new() { Name = "Company B" },
            Supplier = new() { Name = "Company C" }
        });

        OrderList.Add(new() {
            Id = 3,
            Number = "OT002",
            Name = "Yet Another Order",
            LastModified = DateTime.Today,
            Customer = new() { Name = "Company A" },
            Vendor = new() { Name = "Company B" },
            Supplier = new() { Name = "Company C" }
        });

        OnSelectedOrder = ReactiveCommand.Create<OrderListItem>(o => {
            SelectedOrderId = o.Id;
        });

    }

}
