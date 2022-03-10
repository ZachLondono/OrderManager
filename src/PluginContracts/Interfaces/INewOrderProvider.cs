using PluginContracts.Models;

namespace PluginContracts.Interfaces;

public interface INewOrderProvider :IPlugin {

    public OrderDto GetNewOrder();

}