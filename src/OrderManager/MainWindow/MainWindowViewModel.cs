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

    public ReactiveCommand<int, Unit> SelectLineItem { get; }

    public ReactiveCommand<Unit, Unit> OpenNewOrderDialogCommand { get; }

    public MainWindowViewModel() {
        _orderListViewModel = Program.CreateInstance<OrderListViewModel>();
        _orderDetailsViewModel = Program.CreateInstance<OrderDetailsViewModel>();
        SelectLineItem = ReactiveCommand.Create<int>(LineItemSelected);

        OpenNewOrderDialogCommand = ReactiveCommand.Create(OpenNewOrderDialog);
    }

    private void OpenNewOrderDialog() {

        var vm = Program.CreateInstance<NewOrderViewModel>();

        var dialog = new NewOrderDialog {
            DataContext = vm
        };

        dialog.Show();
        // TODO: open this as an actual dialog, not sure how to do that since the MainWindow does not inherit from ReactiveWindow
        //dialog.ShowDialog();
    }

    private async void LineItemSelected(int orderId) {
        await OrderDetailsViewModel.SetOrder(orderId);
    }

}
