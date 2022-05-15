namespace DesktopUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {

    public RibbonViewModel RibbonDataContext {
        get;
        init;
    }

    public MainWindowViewModel(RibbonViewModel ribbonViewModel) {
        RibbonDataContext = ribbonViewModel;
    }

}