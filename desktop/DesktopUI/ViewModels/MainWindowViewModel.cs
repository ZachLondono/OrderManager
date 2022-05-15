using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Orders;
using OrderManager.Domain.Catalog;
using OrderManager.Domain.Orders;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {

    private bool _showLoadingSpinner;
    public bool ShowLoadingSpinner {
        get => _showLoadingSpinner;
        set => this.RaiseAndSetIfChanged(ref _showLoadingSpinner, value);
    }

    private readonly IOrderAPI _orderApi;

    public MainWindowViewModel(IOrderAPI orderApi) {

        _orderApi = orderApi;

        ShowDialog = new Interaction<ToolWindowOpener, Unit>();
        ShowFileDialogAndReturnPath = new Interaction<Unit, string?>();

        ListLabelsCommand = ReactiveCommand.CreateFromTask(OpenLabelListDialog);
        ListEmailsCommand = ReactiveCommand.CreateFromTask(OpenEmailListDialog);
        ListPluginsCommand = ReactiveCommand.CreateFromTask(OpenPluginListDialog);
        ListProfilesCommand = ReactiveCommand.CreateFromTask(OpenProfileListDialog);
        ListCompaniesCommand = ReactiveCommand.CreateFromTask(OpenCompanyListDialog);
        ListProductCommand = ReactiveCommand.CreateFromTask(OpenProductListDialog);
        ToggleSpinnerCommand = ReactiveCommand.Create(() => ShowLoadingSpinner = !ShowLoadingSpinner);
    }

    public Interaction<ToolWindowOpener, Unit> ShowDialog { get; }
    public Interaction<Unit, string?> ShowFileDialogAndReturnPath { get; }

    public ICommand ListLabelsCommand { get; }
    public ICommand ListEmailsCommand { get; }
    public ICommand ListPluginsCommand { get; }
    public ICommand ListProfilesCommand { get; }
    public ICommand ListCompaniesCommand { get; }
    public ICommand ListProductCommand { get; }
    public ICommand ToggleSpinnerCommand { get; }

    private async Task OpenEmailListDialog() {

        var listvm = App.GetRequiredService<EmailListViewModel>();

        await ToolWindowOpener.OpenDialog(new("Email Templates", 450, 375, new EmailListView {
            DataContext = listvm
        }, listvm.LoadData));

    }

    private async Task OpenLabelListDialog() {

        var listvm = App.GetRequiredService<LabelListViewModel>();

        await ToolWindowOpener.OpenDialog(new("Label Templates", 450, 375, new LabelListView {
            DataContext = listvm
        }, listvm.LoadData));

    }

    private async Task OpenPluginListDialog() {

        var listvm = App.GetRequiredService<PluginListViewModel>();
        
        await ToolWindowOpener.OpenDialog(new("Plugins", 450, 375, new PluginListView {
            DataContext = listvm
        }, null));
    }

    private async Task OpenProfileListDialog() {

        var listvm = App.GetRequiredService<ProfileListViewModel>();

        await ToolWindowOpener.OpenDialog(new("Release Profiles", 450, 375, new ProfileListView {
            DataContext = listvm
        }, listvm.LoadData));

    }

    private async Task OpenCompanyListDialog() {

        try {
            var listvm = App.GetRequiredService<CompanyListViewModel>();
            
            await ToolWindowOpener.OpenDialog(new("Companies", 450, 375, new CompanyListView {
                DataContext = listvm
            }, listvm.LoadData));
        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

    private async Task OpenProductListDialog() {

        try {
            var listvm = App.GetRequiredService<ProductListViewModel>();

            await ToolWindowOpener.OpenDialog(new("Products", 450, 375, new ProductListView {
                DataContext = listvm
            }, listvm.LoadData));
        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

}