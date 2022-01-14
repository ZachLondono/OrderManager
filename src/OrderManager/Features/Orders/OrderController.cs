using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Orders;

public class OrderController {

    private readonly ISender _sender;

    public OrderController(ISender sender) {
        _sender = sender;
    }

    public async Task<Order> CreateOrder(string refNum, DateTime orderDate, IEnumerable<LineItem> lineItems) {
        Order order = await _sender.Send(new CreateOrder.Command(refNum, orderDate, lineItems));
        return order;
    }

}
