using DesktopUI.Common;
using DesktopUI.Views;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class RibbonViewModel {

    private IToolWindowContentFactory<EmailListView> _emailListWindowFactory;
    private IToolWindowContentFactory<LabelListView> _labelListWindowFactory;
    private IToolWindowContentFactory<PluginListView> _pluginListWindowFactory;
    private IToolWindowContentFactory<ProfileListView> _profileListWindowFactory;
    private IToolWindowContentFactory<CompanyListView> _companyListWindowFactory;
    private IToolWindowContentFactory<ProductListView> _productListWindowFactory;

    public RibbonViewModel(IToolWindowContentFactory<EmailListView> emailListWindowFactory,
                            IToolWindowContentFactory<LabelListView> labelListWindowFactory,
                            IToolWindowContentFactory<PluginListView> pluginListWindowFactory,
                            IToolWindowContentFactory<ProfileListView> profileListWindowFactory,
                            IToolWindowContentFactory<CompanyListView> companyListWindowFactory,
                            IToolWindowContentFactory<ProductListView> productListWindowFactory) {

        _emailListWindowFactory = emailListWindowFactory;
        _labelListWindowFactory = labelListWindowFactory;
        _pluginListWindowFactory = pluginListWindowFactory;
        _profileListWindowFactory = profileListWindowFactory;
        _companyListWindowFactory = companyListWindowFactory;
        _productListWindowFactory = productListWindowFactory;

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
        await ToolWindowOpener.OpenDialog(_emailListWindowFactory.Create());
    }

    private async Task OpenLabelListDialog() {
        await ToolWindowOpener.OpenDialog(_labelListWindowFactory.Create());
    }

    private async Task OpenPluginListDialog() {
        await ToolWindowOpener.OpenDialog(_pluginListWindowFactory.Create());
    }

    private async Task OpenProfileListDialog() {
        await ToolWindowOpener.OpenDialog(_profileListWindowFactory.Create());
    }

    private async Task OpenCompanyListDialog() {
        await ToolWindowOpener.OpenDialog(_companyListWindowFactory.Create());
    }

    private async Task OpenProductListDialog() {
        await ToolWindowOpener.OpenDialog(_productListWindowFactory.Create());
    }

}
