using Domain.OrderProvider;
using MediatR;
using OrderManager.Shared;
using PluginContracts.Interfaces;
using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.LoadOrders;

public class NewOrderViewModel : ViewModelBase {

    public IEnumerable<string> AvailableProviders { get; set; } = Enumerable.Empty<string>();

    public string SelectedProvider { get; set; } = string.Empty;

    public ReactiveCommand<string, Unit> LoadOrderFromProvider { get; }

    private readonly NewOrderProviderFactory _factory;
    private readonly ISender _sender;

    public NewOrderViewModel(NewOrderProviderFactory factory, ISender sender) {
        LoadOrderFromProvider = ReactiveCommand.Create<string>(OnProviderChosen);
        _factory = factory;
        _sender = sender;
        AvailableProviders = _factory.GetAvailableProviders();
    }

    private void OnProviderChosen(string providerName) {

        INewOrderProvider provider;

        try {
            provider = _factory.GetProviderByName(providerName);
        } catch (KeyNotFoundException e) {
            Debug.WriteLine($"Provider with name '{providerName}' was not found\n" + e);
            return;
        }

        if (provider is not null) {
            var newOrder = provider.GetNewOrder();

            _sender.Send(new UploadNewOrder.Command());

        }

    }

}
