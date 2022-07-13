using ReactiveUI;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public enum View {
    OrderList,
    JobList,
    Scheduler
}

public partial class MainWindowViewModel : ViewModelBase {

    public RibbonViewModel RibbonDataContext { get; init; }
    public MonthlyPlannerViewModel schedulerVM { get; } = new();
    public JobListViewModel jobVM { get; } = new();
    public OrderListViewModel orderVM { get; } = new();

    public ICommand SetOrderListView { get; }
    public ICommand SetJobListView { get; }
    public ICommand SetSchedulerView { get; }

    private View _currentView = View.JobList;
    public View CurrentView {
        get => _currentView;
        set {
            _currentView = value;
            this.RaisePropertyChanged(nameof(IsOrderListVisible));
            this.RaisePropertyChanged(nameof(IsJobListVisible));
            this.RaisePropertyChanged(nameof(IsScheduleVisible));
        }
    }
    
    public bool IsOrderListVisible => CurrentView == View.OrderList;
    public bool IsJobListVisible => CurrentView == View.JobList;
    public bool IsScheduleVisible => CurrentView == View.Scheduler;

    public MainWindowViewModel(RibbonViewModel ribbonViewModel) {
        
        RibbonDataContext = ribbonViewModel;

        SetOrderListView = ReactiveCommand.Create(() => CurrentView = View.OrderList);
        SetJobListView = ReactiveCommand.Create(() => CurrentView = View.JobList);
        SetSchedulerView = ReactiveCommand.Create(() => CurrentView = View.Scheduler);

    }

}