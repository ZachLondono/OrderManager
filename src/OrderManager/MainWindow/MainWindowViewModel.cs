using Microsoft.Extensions.Logging;
using OrderManager.Features.DebugWindow;
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

    public ReactiveCommand<Guid, Unit> SelectLineItem { get; }

    public ReactiveCommand<Unit, Unit> OpenConsoleWindow { get; }

    private readonly ApplicationContext _context = Program.GetService<ApplicationContext>();

    private readonly ILogger<MainWindowViewModel> _logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger) {

        _logger = logger;
        _orderListViewModel = Program.CreateInstance<OrderListViewModel>();
        _orderDetailsViewModel = Program.CreateInstance<OrderDetailsViewModel>();
        SelectLineItem = ReactiveCommand.Create<Guid>(LineItemSelected);

        OpenConsoleWindow = ReactiveCommand.Create(OnConsoleWindowOpen);
    }


    private void OnConsoleWindowOpen() {
        var console = new ConsoleWindow();
        console.Show();
    }

    private void LineItemSelected(Guid orderId) {
        _context.SelectedOrderId = orderId;
        _logger.LogInformation("Order with id selected {0}", orderId);
    }

}
