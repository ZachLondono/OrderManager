using MediatR;
using OrderManager.Features.OrderDetails.EmptyDetails;
using OrderManager.Features.OrderDetails.FilledDetails;
using OrderManager.Shared;
using OrderManager.Shared.DataError;
using ReactiveUI;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderDetails;

public class OrderDetailsViewModel : ViewModelBase {

    private ViewModelBase _content = new EmptyOrderDetailsViewModel();
    public ViewModelBase Content {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    private readonly ISender _sender;
    private readonly ApplicationContext _context;

    public OrderDetailsViewModel(ISender sender, ApplicationContext context) {
        _sender = sender;
        _context = context;

        _context.OrderSelectedEvent += OnOrderSelectedEvent;
    }

    private async Task OnOrderSelectedEvent(object sender, ApplicationContext.OrderSelectedEventArgs e) {
        Debug.WriteLine($"Setting OrderDetailsPage to order [{e.SelectedId}]");
        var result = await _sender.Send(new GetOrderDetails.Query(e.SelectedId));

        result.Match(
            (order) => Content = new FilledOrderDetailsViewModel(_context, order),
            (err) => Content = new DataErrorViewModel(err.Message, err.DetailedMessage));
    }

}
