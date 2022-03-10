using McMaster.NETCore.Plugins;
using PluginContracts.Interfaces;

namespace Domain.Services;

public class PluginService : IPluginService {

    /// <summary>
    /// Stores directories that hold plugins
    /// </summary>
    private readonly ISet<string> _sources;

    /// <summary>
    /// Maps a plugin name to it's loader
    /// </summary>
    private readonly IDictionary<string, PluginLoader> _plugins;

    public event IPluginService.PluginReloadHandler? PluginReloadEvent;

    public PluginService() {
        _sources = new HashSet<string>();
        _plugins = new Dictionary<string, PluginLoader>();
    }

    public void AddSource(string directory) {
        if (_sources.Contains(directory)
            || !IsDirectory(directory)) return;

        _sources.Add(directory);
    }

    public string[] LoadPlugins() {

        foreach (string source in _sources) {

            IEnumerable<string> plugins = Directory.EnumerateDirectories(source);

            foreach (string pluginDirectory in plugins) {

                string pluginName = Path.GetFileName(pluginDirectory); ;

                if (_plugins.ContainsKey(pluginName)) continue;

                string file = Path.Combine(pluginDirectory, $"{pluginName}.dll");

                if (!File.Exists(file)) continue;

                var loader = PluginLoader.CreateFromAssemblyFile(file,
                    sharedTypes: new[] { typeof(IPlugin) },
                    config => config.EnableHotReload = true);

                _plugins.Add(pluginName, loader);

                loader.Reloaded += (object sender, PluginReloadedEventArgs eventArgs) => {
                    PluginReloadEvent?.Invoke(pluginName, eventArgs);
                };

            }

        }

        return _plugins.Keys.ToArray();

    }

    public Type[] GetPluginTypes<T>() where T : IPlugin {

        List<Type> plugins = new();

        foreach (var loader in _plugins.Values) {
            plugins.AddRange(loader.LoadDefaultAssembly()
                                    .GetTypes()
                                    .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract));
        }

        return plugins.ToArray();

    }

    private static bool IsDirectory(string directory) {
        if (!string.IsNullOrEmpty(Path.GetFileName(directory))
            || !Directory.Exists(directory)) return false;
        return true;
    }

}
