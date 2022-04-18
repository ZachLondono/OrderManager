using McMaster.NETCore.Plugins;
using OrderManager.ApplicationCore.Plugins;
using OrderManager.Domain.Plugins;
using PluginContracts.Interfaces;
using System.Text.Json;

namespace Infrastructure.Plugins;

public class PluginManager : IPluginManager {

    private readonly List<Plugin> _plugins = new();

    public IEnumerable<Plugin> GetPluginTypes() => _plugins;

    public Task LoadPluginsFromPath(string path) {

        IEnumerable<string> plugins = Directory.EnumerateDirectories(path);

        foreach (string pluginDirectory in plugins) {

            string assemblyName = Path.GetFileName(pluginDirectory); ;

            string file = Path.Combine(pluginDirectory, $"{assemblyName}.dll");
            string settingsFile = Path.Combine(pluginDirectory, $"{assemblyName}-settings.json");

            if (!File.Exists(settingsFile) || !File.Exists(file)) continue;

            var settingsByName = JsonSerializer.Deserialize<Dictionary<string, PluginSettings>>(File.ReadAllText(settingsFile));
            if (settingsByName is null) continue;

            if (!File.Exists(file)) continue;

            var loader = PluginLoader.CreateFromAssemblyFile(file,
                sharedTypes: new[] { typeof(IPlugin) },
                config => config.EnableHotReload = true);
            
            IEnumerable<Type> types = loader.LoadDefaultAssembly()
                                            .GetTypes()
                                            .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var t in types) {

                if (t.FullName is null) continue;
                if (!settingsByName.ContainsKey(t.FullName)) continue;

                var settings = settingsByName[t.FullName];
                _plugins.Add(new(settings.Name, settings.Version, file, t));
            }

        }

        return Task.CompletedTask;

    }

    private class PluginSettings {

        public string Name { get; set; } = string.Empty;

        public int Version { get; set; }

    }

}
