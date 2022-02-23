﻿using MediatR;
using OrderManager.Shared;
using OrderManager.Shared.Messages;
using PluginContracts.Interfaces;
using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.LoadOrders;

public class NewOrderViewModel : ViewModelBase {

    public IEnumerable<string> AvailableProviders { get; init; }

    public string SelectedProvider { get; set; } = string.Empty;

    public ReactiveCommand<string, Unit> LoadOrderFromProvider { get; }

    private readonly ISender _sender;

    public NewOrderViewModel(NewOrderProviderFactory factory, ISender sender) {
        _sender = sender;
        AvailableProviders = factory.GetAvailableProviders();

        LoadOrderFromProvider = ReactiveCommand.Create<string>(OnProviderChosen);
    }

    private async void OnProviderChosen(string providerName) {
        var id = await _sender.Send(new UploadNewOrder.Command(providerName));
        MessageBus.Current.SendMessage(new OrderUploaded(id));
    }

}
