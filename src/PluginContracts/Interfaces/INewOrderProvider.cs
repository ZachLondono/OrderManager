using PluginContracts.Models;

namespace PluginContracts.Interfaces;

public interface INewOrderProvider {
    public string ProviderName { get; }
    public NewOrder GetNewOrder();
}