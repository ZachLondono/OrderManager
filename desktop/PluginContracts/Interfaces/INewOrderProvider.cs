using PluginContracts.Models;

namespace PluginContracts.Interfaces;

public interface INewOrderProvider :IPlugin {

    public OrderDto GetNewOrder();

}

public interface INewOrderFromFileProvider : IPlugin {

    public OrderDto GetNewOrder(string filePath);

}