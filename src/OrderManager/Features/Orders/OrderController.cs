using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Orders;

public class OrderController : BaseController {

    public OrderController(ISender sender) : base(sender) { }

    public async Task<Order?> CreateOrder(string refNum, DateTime orderDate) {
        return await Sender.Send(new CreateOrder.Command(refNum, orderDate));
    }

    public async Task<IEnumerable<Order>> GetAllOrders() {
        return await Sender.Send(new GetAllOrders.Query());
    }

}
