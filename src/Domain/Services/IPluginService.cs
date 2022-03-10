using McMaster.NETCore.Plugins;
using PluginContracts.Interfaces;

namespace Domain.Services;

public interface IPluginService {

    /// <summary>
    /// Event is invoked whenever any of the plugins are reloaded
    /// </summary>
    public event PluginReloadHandler? PluginReloadEvent;
    public delegate void PluginReloadHandler(string pluginName, PluginReloadedEventArgs e);

    /// <summary>
    /// Add a directory in which to look for plugins
    /// </summary>
    /// <param name="directory">Directory which contains plugins</param>
    public void AddSource(string directory);

    /// <summary>
    /// Reloads all plugins from all sources
    /// </summary>
    /// <returns>A list of plugin names</returns>
    public string[] LoadPlugins();

    /// <summary>
    /// Get types of all loaded plugins which implement a given type
    /// </summary>
    /// <typeparam name="T">Type of plugin</typeparam>
    /// <returns>An array of concrete plugins of the given type</returns>
    public Type[] GetPluginTypes<T>() where T : IPlugin;

}