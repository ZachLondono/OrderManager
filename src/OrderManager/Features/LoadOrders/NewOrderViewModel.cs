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

    private string _selectedVersion = string.Empty;
    public string SelectedProviderVersion {
        get => _selectedVersion;
        set => this.RaiseAndSetIfChanged(ref _selectedVersion, value);
    }

    private string _selectedProvider = string.Empty;
    public string SelectedProvider { 
        get => _selectedProvider;
        set {
            _selectedProvider = value;
            var provider = _factory.GetProviderByName(value);
            SelectedProviderVersion = $"v{provider.Version}";
        }
    }

    public ReactiveCommand<string, Unit> LoadOrderFromProvider { get; }

    private readonly ISender _sender;
    private readonly ApplicationContext _context;
    private readonly ILogger<NewOrderViewModel> _logger;
    private readonly NewOrderProviderFactory _factory;

    public NewOrderViewModel(ILogger<NewOrderViewModel> logger, NewOrderProviderFactory factory, ISender sender, ApplicationContext context) { 
        _logger = logger;
        _factory = factory;
        _sender = sender;
        _context = context;

        AvailableProviders = factory.GetAvailableProviders();

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
