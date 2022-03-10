using MediatR;
using OrderManager.Features.OrderList.FilledList;
using OrderManager.Features.OrderList.EmptyList;
using OrderManager.Shared;
using ReactiveUI;
using System.Threading.Tasks;
using OrderManager.Shared.DataError;
using System;
using System.Diagnostics;

namespace OrderManager.Features.OrderList;

public class OrderListViewModel : ViewModelBase {

    private ViewModelBase _content = new EmptyOrderListViewModel();
    public ViewModelBase Content {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    private readonly ISender _sender;

    public OrderListViewModel(ISender sender, ApplicationContext context) {
        _sender = sender;
        _ = Task.Run(async () => await RefreshContent());

        context.OrderAddedEvent += OnOrderAddedEvent;
    }

    private async Task OnOrderAddedEvent(object sender, ApplicationContext.OrderAddedEventArgs e) {
        //TODO: if an order is added, don't refresh all content, just add the new order to the list
        await RefreshContent();
    }

    private async Task RefreshContent() {
        try {
            var result = await _sender.Send(new GetOrders.Query());

            result.Match(
                (data) => Content = new FilledOrderListViewModel(data.Orders),
                (err) => Content = new DataErrorViewModel(err.Message, err.DetailedMessage));
        } catch (Exception e) {
            Debug.WriteLine(e);
        }
    }

}
