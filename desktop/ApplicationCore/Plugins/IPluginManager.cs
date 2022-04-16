using OrderManager.Domain.Plugins;

namespace OrderManager.ApplicationCore.Plugins;

public interface IPluginManager {

    public Task LoadPluginsFromPath(string path);
    
    public IEnumerable<Plugin> GetPluginTypes();

}
