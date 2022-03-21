using OrderManager.Shared;
using ReactiveUI;
using System.Reactive;

namespace OrderManager.Features.Ribbon.DeleteOrder;

public class DeleteOrderViewModel : ViewModelBase {

    public ReactiveCommand<Unit, object> DeleteOrderCommand { get; }

    public ReactiveCommand<Unit, object> CancelDeleteCommand { get; }

    public string Message { get => $"Are you sure you want to delete order {OrderId}?"; }

    public string OrderId { get; set; } = string.Empty;

    public DeleteOrderViewModel() {

        DeleteOrderCommand = ReactiveCommand.Create(() => (object) true);
        CancelDeleteCommand = ReactiveCommand.Create(() => (object) false);

    }

}
