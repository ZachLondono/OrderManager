﻿using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using static OrderManager.Features.OrderList.GetOrders;

namespace OrderManager.Features.OrderList.FilledList;

public class FilledOrderListViewModel : ViewModelBase {

    public ObservableCollection<OrderFilterOptionViewModel> OrderFilters { get; set; }

    public ObservableCollection<ListItemViewModel> Items { get; set; }

    public ReactiveCommand<Unit, Unit> OrderDirectionCommand { get; set; }

    public FilledOrderListViewModel(IEnumerable<OrderListItem> items) {

        items = items.OrderByDescending(i => i.LastModified);

        OrderDirectionCommand = ReactiveCommand.Create(() => {

        });

        List<ListItemViewModel> list = new();
        foreach (var item in items) {
            list.Add(new() {
                Id = item.Id,
                Number = item.Number,
                Name = item.Name,
                IsPriority = item.IsPriority,
                LastModified = item.LastModified,
                CompanyNames = $"{item.CustomerName} / {item.VendorName} / {item.SupplierName}"
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

    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsPriority { get; set; } = false;

    public DateTime LastModified { get; set; } = DateTime.Now;

    public string DateModifiedStr {
        get => DateTime.Today.IsSameWeek(LastModified) ? LastModified.ToString("ddd h:mm") : LastModified.ToString("d/M/yy h:mm");
    }

    public string CompanyNames { get; set; } = string.Empty;

}