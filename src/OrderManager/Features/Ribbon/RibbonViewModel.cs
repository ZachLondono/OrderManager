using OrderManager.Features.LoadOrders;
using OrderManager.Shared;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace OrderManager.Features.Ribbon;

public class RibbonViewModel : ViewModelBase {

    public Interaction<NewOrderViewModel, Unit> ShowDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenNewOrderDialogCommand { get; }

    public RibbonViewModel() {
        ShowDialog = new();
        OpenNewOrderDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            var vm = Program.CreateInstance<NewOrderViewModel>();
            await ShowDialog.Handle(vm);
        });

    }

}
