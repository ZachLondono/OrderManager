using McMaster.NETCore.Plugins;
using Microsoft.Extensions.Logging;
using PluginContracts.Interfaces;
using System.Diagnostics;

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

    private readonly ILogger<PluginService> _logger;

    public PluginService(ILogger<PluginService> logger) {

        _logger = logger;

        _sources = new HashSet<string>();
        _plugins = new Dictionary<string, PluginLoader>();

        var assembly = System.Reflection.Assembly.GetEntryAssembly();
        if (assembly is not null) {
            var baseDir = Path.GetDirectoryName(assembly.Location);
            if (baseDir is not null) {
                string defaultSource = Path.Combine(baseDir, "plugins");
                AddSource(defaultSource);
            }
        }

        LoadPlugins();

    }

    public void AddSource(string directory) {
        if (_sources.Contains(directory)
            || !IsDirectory(directory)) return;

        _logger.LogDebug("Adding plugin source {0}", directory);
        _sources.Add(directory);
    }

    public string[] LoadPlugins() {

        foreach (string source in _sources) {

            IEnumerable<string> plugins = Directory.EnumerateDirectories(source);

            foreach (string pluginDirectory in plugins) {

                Console.WriteLine($"Loading plugin from directory {pluginDirectory}");
                _logger.LogDebug("Loading plugin from directory {0}", pluginDirectory);

                string assemblyName = Path.GetFileName(pluginDirectory); ;

                if (_plugins.ContainsKey(assemblyName)) continue;

                string file = Path.Combine(pluginDirectory, $"{assemblyName}.dll");

                if (!File.Exists(file)) continue;

                var loader = PluginLoader.CreateFromAssemblyFile(file,
                    sharedTypes: new[] { typeof(IPlugin) },
                    config => config.EnableHotReload = true);

                _plugins.Add(assemblyName, loader);

                loader.Reloaded += (object sender, PluginReloadedEventArgs eventArgs) => {
                    try {
                        
                        PluginReloadEvent?.Invoke(assemblyName, eventArgs);
                        Console.WriteLine($"Plugin reloaded: {assemblyName}");
                        _logger.LogInformation("Plugin reloaded: '{0}'", assemblyName);

                    } catch(Exception e) {
                        _logger.LogError("Error invoking reload event {0}", e);
                    }
                };

            }

        }

        _logger.LogInformation("Plugins loaded: {0}", _plugins.Keys);

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

    public Type[] GetPluginTypesFromAssembly<T>(string assemblyName) where T : IPlugin {

        if (!_plugins.ContainsKey(assemblyName)) return new Type[0];

        return _plugins[assemblyName].LoadDefaultAssembly()
                .GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

    }

    private static bool IsDirectory(string directory) =>
        string.IsNullOrEmpty(Path.GetFileName(directory))
            || Directory.Exists(directory);

}
