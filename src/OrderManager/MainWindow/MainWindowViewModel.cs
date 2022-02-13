using Avalonia.Controls;
using MediatR;
using OrderManager.Features.OrderDetails;
using OrderManager.Features.OrderList;
using OrderManager.Shared;
using ReactiveUI;
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

    public MainWindowViewModel(ISender sender) {
        _orderListViewModel = new(sender);
        _orderDetailsViewModel = new(sender);
        SelectLineItem = ReactiveCommand.Create<int>(LineItemSelected);
    }

    private async void LineItemSelected(int orderId) {
        await OrderDetailsViewModel.SetOrder(orderId);
    }

}
