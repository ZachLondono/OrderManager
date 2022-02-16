using Avalonia.Controls;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Features.OrderDetails;
using OrderManager.Features.OrderList;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Diagnostics;
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

    public ReactiveCommand<int, Unit> SelectLineItem { get; }

    public MainWindowViewModel() {
        if (Program.ServiceProvider is null) throw new InvalidProgramException("ServiceProvider is null");

        ISender? sender = Program.ServiceProvider?.GetService<ISender>();

        if (sender is null) throw new InvalidProgramException("Unable to get implementation of ISender");

        _orderListViewModel = new(sender);
        _orderDetailsViewModel = new(sender);
        SelectLineItem = ReactiveCommand.Create<int>(LineItemSelected);
    }

    private async void LineItemSelected(int orderId) {
        await OrderDetailsViewModel.SetOrder(orderId);
    }

}
