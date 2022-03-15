using Domain.Services;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.Logging;
using PluginContracts.Interfaces;
using System;
using System.Collections.Generic;

namespace OrderManager.Features.LoadOrders;

public class NewOrderProviderFactory {

    private readonly Dictionary<string, IPlugin> _providers = new();

    private readonly ILogger<NewOrderProviderFactory> _logger;
    private readonly IPluginService _pluginService;

    public NewOrderProviderFactory(ILogger<NewOrderProviderFactory> logger, IPluginService pluginService) {
        _logger = logger;
        _pluginService = pluginService;

        _pluginService.PluginReloadEvent += _pluginService_PluginReloadEvent;

        Type[] providerTypes = _pluginService.GetPluginTypes<IPlugin>();
        UpdateProviderList(providerTypes);

    }

    private void _pluginService_PluginReloadEvent(string assemblyName, PluginReloadedEventArgs e) {
        Type[] providerTypes = _pluginService.GetPluginTypesFromAssembly<INewOrderProvider>(assemblyName);
        UpdateProviderList(providerTypes);
    }

    /// <summary>
    /// Creates an instance of the concrete classes which implement the INewProvider interface and add them to the list of providers, if they do not already exist, or are newer versions
    /// </summary>
    /// <param name="providerTypes">List of concrete provider types</param>
    private void UpdateProviderList(Type[] providerTypes) {
        foreach (Type providerType in providerTypes) {

            if (providerType.IsAbstract || (!typeof(INewOrderProvider).IsAssignableFrom(providerType) && !typeof(INewOrderFromFileProvider).IsAssignableFrom(providerType))) continue;

            try {

                IPlugin? provider = null;

                if (typeof(INewOrderFromFileProvider).IsAssignableFrom(providerType)) {
                    provider = Program.GetInstance(providerType) as INewOrderFromFileProvider;
                } else if (typeof(INewOrderProvider).IsAssignableFrom(providerType)) {
                    provider = Program.GetInstance(providerType) as INewOrderProvider;
                }

                if (provider is null) return;

                if (_providers.ContainsKey(provider.PluginName)) {
                    var existing = _providers[provider.PluginName];
                    if (existing.Version >= provider.Version) continue;
                }

                _providers[provider.PluginName] = provider;

            } catch (Exception e){
                _logger.LogDebug("Error updating order provider\n{0}", e);
            }

        }
    }

    public IPlugin GetProviderByName(string providerName) {
        return _providers[providerName];
    }

    public void RemoveProvider(INewOrderProvider provider) {
        _providers.Remove(provider.PluginName);
    }

    public IEnumerable<string> GetAvailableProviders() {
        return _providers.Keys;
    }

}