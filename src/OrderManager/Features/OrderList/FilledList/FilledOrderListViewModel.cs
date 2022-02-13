using Avalonia.Interactivity;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.OrderList.FilledList;

public class FilledOrderListViewModel : ViewModelBase {

    public ObservableCollection<OrderModel> Items { get; set; }

    public FilledOrderListViewModel(IEnumerable<OrderModel> items) {
        Items = new ObservableCollection<OrderModel>(items);
    }

}