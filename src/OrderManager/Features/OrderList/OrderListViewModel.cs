using MediatR;
using OrderManager.Features.OrderList.FilledList;
using OrderManager.Features.OrderList.EmptyList;
using OrderManager.Shared;
using ReactiveUI;
using System.Threading.Tasks;
using OrderManager.Shared.DataError;
using System;
using OrderManager.Shared.Messages;

namespace OrderManager.Features.OrderList;

public class OrderListViewModel : ViewModelBase {

    private ViewModelBase _content = new EmptyOrderListViewModel();
    public ViewModelBase Content {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    private readonly ISender _sender;

    public OrderListViewModel(ISender sender) {
        _sender = sender;
        _ = Task.Run(async () => await RefreshContent());

        MessageBus.Current
            .Listen<OrderUploaded>()
            .WhereNotNull()
            .Subscribe(async x => {
                await RefreshContent();
            });

    }

    private async Task RefreshContent() {
        var result = await _sender.Send(new GetOrders.Query());

        result.Match(
            (data) => Content = new FilledOrderListViewModel(data.Orders),
            (err) => Content = new DataErrorViewModel(err.Message, err.DetailedMessage));
        
    }

}
