using MediatR;
using Microsoft.Extensions.Logging;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.LoadOrders;

public class NewOrderViewModel : ViewModelBase {

    public IEnumerable<string> AvailableProviders { get; init; }

    public string SelectedProvider { get; set; } = string.Empty;

    public ReactiveCommand<string, Unit> LoadOrderFromProvider { get; }

    private readonly ISender _sender;
    private readonly ApplicationContext _context;
    private readonly ILogger<NewOrderViewModel> _logger;

    public NewOrderViewModel(ILogger<NewOrderViewModel> logger, NewOrderProviderFactory factory, ISender sender, ApplicationContext context) {
        AvailableProviders = factory.GetAvailableProviders();
        _sender = sender;
        _context = context;
        _logger = logger;

        LoadOrderFromProvider = ReactiveCommand.Create<string>(OnProviderChosen);
    }

    private void OnProviderChosen(string providerName) {
        try {

            var id = _sender.Send(new UploadNewOrder.Command(providerName)).Result;
            _context.AddOrder(id);
            _context.SelectedOrderId = id;

        } catch (Exception e) {
            _logger.LogError("Could not upload new order from provider '{0}'\n{1}", providerName, e);
        }
    }

}
