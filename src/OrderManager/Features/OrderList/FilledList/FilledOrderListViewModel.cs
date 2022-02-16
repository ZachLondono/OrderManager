using Domain.Entities.OrderAggregate;
using OrderManager.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrderManager.Features.OrderList.FilledList;

public class FilledOrderListViewModel : ViewModelBase {

    public ObservableCollection<OrderFilterOptionViewModel> OrderFilters { get; set; }

    public ObservableCollection<ListItemViewModel> Items { get; set; }

    public FilledOrderListViewModel(IEnumerable<Order> items) {

        List<ListItemViewModel> list = new();
        foreach (var item in items) {
            list.Add(new() {
                Id = item.Id,
                Number = item.Number,
                Name = item.Name,
                IsPriority = item.IsPriority,
                LastModified = item.LastModified,
                CompanyNames = $"{item.Customer?.Name} / {item.Vendor?.Name} / {item.Supplier?.Name}"
            });        
        }
        
        Items = new ObservableCollection<ListItemViewModel>(list);

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

    }

}

public class ListItemViewModel {

    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsPriority { get; set; } = false;

    public DateTime LastModified { get; set; } = DateTime.Now;

    public string DateModifiedStr {
        get => DateTime.Today.IsSameWeek(LastModified) ? LastModified.ToString("ddd h:mm") : LastModified.ToString("d/M/yy h:mm");
    }

    public string CompanyNames { get; set; } = string.Empty;

}