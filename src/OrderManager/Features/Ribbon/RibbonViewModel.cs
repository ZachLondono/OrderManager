using OrderManager.Features.LoadOrders;
using OrderManager.Features.ProductDesigner;
using OrderManager.Features.RemakeOrder;
using OrderManager.Shared;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon;

public class RibbonViewModel : ViewModelBase {

    public Interaction<NewOrderViewModel, Unit> ShowNewOrderDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenNewOrderDialogCommand { get; }

    public Interaction<ProductDesignerViewModel, Unit> ShowNewProductDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenProductDesignerDialogCommand { get; }

    public Interaction<OrderRemakeViewModel, Unit> ShowOrderRemakeDialog { get; }
    public ReactiveCommand<Unit, Unit> RemakeCurrentOrderCommand { get; }

    private bool _isOrderSelected;
    private bool IsOrderSelected {
        get => _isOrderSelected;
        set => this.RaiseAndSetIfChanged(ref _isOrderSelected, value);
    }

    private readonly ApplicationContext _context;

    public RibbonViewModel(ApplicationContext context) {

        _context = context;
        _context.OrderSelectedEvent += (a, b) => {
            IsOrderSelected = true;
            return Task.CompletedTask;
        };

        ShowNewOrderDialog = new();
        OpenNewOrderDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            var vm = Program.CreateInstance<NewOrderViewModel>();
            await ShowNewOrderDialog.Handle(vm);
        });

        ShowNewProductDialog = new();
        OpenProductDesignerDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            var vm = new ProductDesignerViewModel() {
                Attributes = {
                    new() { Name = "Height", DefaultValue = "4.125"},
                    new() { Name = "Width", DefaultValue = "21"},
                    new() { Name = "Depth", DefaultValue = "21"}
                }
            };
            await ShowNewProductDialog.Handle(vm);
        });

        var orderSelected = this.WhenAnyValue(x => x.IsOrderSelected);

        ShowOrderRemakeDialog = new();
        RemakeCurrentOrderCommand = ReactiveCommand.CreateFromTask(async () => {
            var vm = new OrderRemakeViewModel();
            await ShowOrderRemakeDialog.Handle(vm);
        }, canExecute: orderSelected);

    }

}
