using McMaster.NETCore.Plugins;
using OrderManager.ApplicationCore.Common;
using OrderManager.ApplicationCore.Plugins;
using OrderManager.Domain.Plugins;
using PluginContracts.Interfaces;
using System.Text.Json;

namespace Infrastructure.Plugins;

public class PluginManager : IPluginManager {

    private readonly Dictionary<string, Plugin> _plugins = new();
    private readonly IFileIO _fileIO;

    public PluginManager(IFileIO fileIO) {
        _fileIO = fileIO;
    }

    public IEnumerable<Plugin> GetPluginTypes() => _plugins.Values;

    public Task LoadPluginsFromPath(string path) {

        var validPlugins = GetValidPluginSettings(path);

        foreach (var plugin in validPlugins) {
            var assemblyFile = plugin.Key;
            IEnumerable<Type> types = PluginLoader.CreateFromAssemblyFile(assemblyFile,
                                                    sharedTypes: new[] { typeof(IPlugin) },
                                                    config => config.EnableHotReload = true)
                                                .LoadDefaultAssembly()
                                                .GetTypes()
                                                .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);

            var settingsByName = plugin.Value;
            foreach (var t in types) {
                if (t.FullName is null || settingsByName.PluginSettings is null) continue;
                if (!settingsByName.PluginSettings.ContainsKey(t.FullName)) continue;

                var settings = settingsByName.PluginSettings[t.FullName];

                // If a plugin with the same name is already loaded, keep which ever instance has the higher version
                if (_plugins.ContainsKey(settings.Name) && _plugins[settings.Name].Version > settings.Version) {
                    continue;
                }

                _plugins.Add(settings.Name, new(settings.Name, settings.Version, assemblyFile, t));
            }

        }


        return Task.CompletedTask;

    }

    public Dictionary<string, AssemblySettings> GetValidPluginSettings(string path) {

        var settingsByFile = new Dictionary<string, AssemblySettings>();

        IEnumerable<string> plugins = _fileIO.EnumerateDirectories(path);

        foreach (string pluginDirectory in plugins) {

            string assemblyName = Path.GetFileName(pluginDirectory);
            string assemblyFile = Path.Combine(pluginDirectory, $"{assemblyName}.dll");
            string settingsFile = Path.Combine(pluginDirectory, $"{assemblyName}-settings.json");

            if (!_fileIO.Exists(settingsFile) || !_fileIO.Exists(assemblyFile)) continue;

            try {
                var assemblySettings = JsonSerializer.Deserialize<AssemblySettings>(_fileIO.ReadAllText(settingsFile));
                if (assemblySettings is null || assemblySettings.PluginSettings is null) continue;

                settingsByFile[assemblyFile] = assemblySettings;

            } catch { }

        }

        return settingsByFile;

    }

    public class AssemblySettings {

        public Dictionary<string, PluginSettings>? PluginSettings { get; set; }

    }

    public class PluginSettings {

        public string Name { get; set; } = string.Empty;

        public int Version { get; set; }

    }

}
