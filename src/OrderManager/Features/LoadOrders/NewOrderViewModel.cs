using MediatR;
using OrderManager.Shared;
using ReactiveUI;
using System.Collections.Generic;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.LoadOrders;

public class NewOrderViewModel : ViewModelBase {

    public IEnumerable<string> AvailableProviders { get; init; }

    public string SelectedProvider { get; set; } = string.Empty;

    public ReactiveCommand<string, Unit> LoadOrderFromProvider { get; }

    private readonly ISender _sender;
    private readonly ApplicationContext _context;

    public NewOrderViewModel(NewOrderProviderFactory factory, ISender sender, ApplicationContext context) {
        AvailableProviders = factory.GetAvailableProviders();
        _sender = sender;
        _context = context;

        LoadOrderFromProvider = ReactiveCommand.Create<string>(OnProviderChosen);
    }

    private async void OnProviderChosen(string providerName) {
        var id = await _sender.Send(new UploadNewOrder.Command(providerName));
        _context.SelectedOrderId = id;
        _context.AddOrder(id);
    }

}
