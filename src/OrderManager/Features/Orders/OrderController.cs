using MediatR;
using OrderManagment.Models;

namespace OrderManagment.Features.Orders;

public class OrderController {

    private readonly ISender _sender;

    public OrderController(ISender sender) {
        _sender = sender;
    }

    public async Task<Order> CreateOrder(string refNum, DateTime orderDate, IEnumerable<Order.LineItem> lineItems) {
        Order order = await _sender.Send(new Create.Command(refNum, orderDate, lineItems));
        return order;
    }

}
