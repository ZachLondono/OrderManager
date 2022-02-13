using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public class FilledOrderDetailsViewModel : ViewModelBase {

    public OrderModel Order { get; set; }

    public string ReleaseBtnText {
        get {
            if (Order is null || Order.ReleaseDate is null)
                return "Release";
            else
                return $"Re-release\n{Order.ReleaseDate?.ToString("MM/dd/yy H:mm") ?? ""}";
        }
    }
    
    public ReactiveCommand<int, Unit> ReleaseOrder { get; }

    public FilledOrderDetailsViewModel(OrderModel order) {
        Order = order;
        ReleaseOrder = ReactiveCommand.Create<int>(OnOrderRelease);
    }

    public void OnOrderRelease(int orderId) {
        Order.ReleaseDate = DateTime.Now;
        this.RaisePropertyChanged(nameof(ReleaseBtnText));
        Debug.WriteLine($"Releasing order {orderId}");
    }

}
