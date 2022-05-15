using DesktopUI.Common;
using DesktopUI.Views;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class RibbonViewModel {

    public RibbonViewModel() {

        ListLabelsCommand = ReactiveCommand.CreateFromTask(OpenLabelListDialog);
        ListEmailsCommand = ReactiveCommand.CreateFromTask(OpenEmailListDialog);
        ListPluginsCommand = ReactiveCommand.CreateFromTask(OpenPluginListDialog);
        ListProfilesCommand = ReactiveCommand.CreateFromTask(OpenProfileListDialog);
        ListCompaniesCommand = ReactiveCommand.CreateFromTask(OpenCompanyListDialog);
        ListProductCommand = ReactiveCommand.CreateFromTask(OpenProductListDialog);

    }

    public ICommand ListLabelsCommand { get; }
    public ICommand ListEmailsCommand { get; }
    public ICommand ListPluginsCommand { get; }
    public ICommand ListProfilesCommand { get; }
    public ICommand ListCompaniesCommand { get; }
    public ICommand ListProductCommand { get; }

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
