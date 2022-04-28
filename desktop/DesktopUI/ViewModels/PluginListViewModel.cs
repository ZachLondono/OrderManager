using OrderManager.ApplicationCore.Plugins;
using OrderManager.Domain.Plugins;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class PluginListViewModel : ViewModelBase {

    public ObservableCollection<Plugin> Plugins { get; set; } = new ();

    private readonly IPluginManager _pluginMngr;

    public PluginListViewModel(IPluginManager pluginMngr) {
        _pluginMngr = pluginMngr;
        AddSourceCommand = ReactiveCommand.CreateFromTask(OnAddSource);
    }

    public ICommand AddSourceCommand;

    public Task OnAddSource() => throw new NotImplementedException();

    /// <summary>
    /// Loads plugins from all saved plugin sources and updates the Plugins list
    /// </summary>
    public Task LoadPluginsFromSources() => throw new NotImplementedException();

}
