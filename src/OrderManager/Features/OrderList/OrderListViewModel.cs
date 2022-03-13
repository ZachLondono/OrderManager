using MediatR;
using OrderManager.Features.OrderList.FilledList;
using OrderManager.Features.OrderList.EmptyList;
using OrderManager.Shared;
using ReactiveUI;
using System.Threading.Tasks;
using OrderManager.Shared.DataError;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace OrderManager.Features.OrderList;

public class OrderListViewModel : ViewModelBase {

    private ViewModelBase _content = new EmptyOrderListViewModel();
    public ViewModelBase Content {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
        //private set => _content = value;
    }

    private readonly ISender _sender;
    private readonly ILogger<OrderListViewModel> _logger;

    public OrderListViewModel(ILogger<OrderListViewModel> logger, ISender sender, ApplicationContext context) {
        _logger = logger;
        _sender = sender;

        RefreshContent();

        context.OrderListUpdateEvent += OnOrderAddedEvent;
    }

    private Task OnOrderAddedEvent(object sender, ApplicationContext.OrderAddedEventArgs e) {
        //TODO: if an order is added, don't refresh all content, just add the new order to the list
        return Task.Run(() => RefreshContent());
    }

    private void RefreshContent() {
        try {

            _logger.LogDebug("Refreshing order list");

            var result = _sender.Send(new GetOrders.Query()).Result;

            ViewModelBase? newContent = null;

            result.Match(
                (data) => newContent = new FilledOrderListViewModel(data.Orders),
                (err) => newContent = new DataErrorViewModel(err.Message, err.DetailedMessage));

            _logger.LogTrace("Setting OrderListView Content");
            if (newContent is not null)
                Content = newContent;

        } catch (Exception e) {
            _logger.LogError("Could not refresh content\n{0}", e);
        }
    }

}
