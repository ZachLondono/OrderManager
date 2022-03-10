using PluginContracts.Interfaces;
using System;
using System.Collections.Generic;

namespace OrderManager.Features.LoadOrders;

public class NewOrderProviderFactory {

    private readonly Dictionary<string, INewOrderProvider> _providers = new();

    public INewOrderProvider GetProviderByName(string providerName) {
        return _providers[providerName];
    }

    public void AddProvider(INewOrderProvider provider) {
        if (_providers.ContainsKey(provider.PluginName))
            throw new ArgumentException($"Provider with name '{provider.PluginName}' already exists");

        _providers.Add(provider.PluginName, provider);
    }

    public void RemoveProvider(INewOrderProvider provider) {
        _providers.Remove(provider.PluginName);
    }

    public IEnumerable<string> GetAvailableProviders() {
        return _providers.Keys;
    }

}