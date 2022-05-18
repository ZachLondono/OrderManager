namespace DesktopUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {

    public RibbonViewModel RibbonDataContext { get; init; }

    public OrderListViewModel OrderListViewModel { get; init; }

    public MainWindowViewModel(RibbonViewModel ribbonViewModel, OrderListViewModel orderListViewModel) {
        RibbonDataContext = ribbonViewModel;
        OrderListViewModel = orderListViewModel;
    }

}