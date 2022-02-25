using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Features.LoadOrders;
using OrderManager.Features.OrderDetails;
using OrderManager.Features.OrderList;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using Unit = System.Reactive.Unit;

namespace OrderManager.MainWindow;
public class MainWindowViewModel : ViewModelBase {

    private OrderListViewModel _orderListViewModel;
    public OrderListViewModel OrderListViewModel {
        get => _orderListViewModel;
        set => this.RaiseAndSetIfChanged(ref _orderListViewModel, value);
    }

    private OrderDetailsViewModel _orderDetailsViewModel;
    public OrderDetailsViewModel OrderDetailsViewModel {
        get => _orderDetailsViewModel;
        set => this.RaiseAndSetIfChanged(ref _orderDetailsViewModel, value);
    }

    public ReactiveCommand<Guid, Unit> SelectLineItem { get; }

    public MainWindowViewModel() {
        _orderListViewModel = Program.CreateInstance<OrderListViewModel>();
        _orderDetailsViewModel = Program.CreateInstance<OrderDetailsViewModel>();
        SelectLineItem = ReactiveCommand.Create<Guid>(LineItemSelected);
    }

    private async void LineItemSelected(Guid orderId) {
        await OrderDetailsViewModel.SetOrder(orderId);
    }

}
