using PluginContracts.Models;

namespace PluginContracts.Interfaces;

public interface IReleaseAction : IPlugin {

    /// <summary>
    /// Does some action on the order 
    /// </summary>
    /// <param name="orderDto"></param>
    public void Run(OrderDto orderDto);

}
