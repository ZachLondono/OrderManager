using OrderManager.Domain.Orders;

namespace PluginContracts.Interfaces;

public interface IReleaseAction : IPlugin {

    public Task Run(Order order);

}
