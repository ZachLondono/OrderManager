using MediatR;
using OrderManager.Features.OrderDetails.EmptyDetails;
using OrderManager.Features.OrderDetails.FilledDetails;
using OrderManager.Shared;
using OrderManager.Shared.DataError;
using OrderManager.Shared.Messages;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderDetails;

public class OrderDetailsViewModel : ViewModelBase {

    private ViewModelBase _content = new EmptyOrderDetailsViewModel();
    public ViewModelBase Content {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    private readonly ISender _sender;

    public OrderDetailsViewModel(ISender sender) {
        _sender = sender;

        MessageBus.Current
            .Listen<OrderUploaded>()
            .WhereNotNull()
            .Subscribe(async x => {
                await SetOrder(x.OrderId);
            });
    }

    public async Task SetOrder(Guid orderId) {
        Debug.WriteLine($"Setting OrderDetailsPage to order [{orderId}]");
        var result = await _sender.Send(new GetOrderDetails.Query(orderId));
        
        result.Match(
            (order) => Content = new FilledOrderDetailsViewModel(order),
            (err) => Content = new DataErrorViewModel(err.Message, err.DetailedMessage));
    }

}
