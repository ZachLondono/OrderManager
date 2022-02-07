using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Orders;

public class OrderController : BaseController {

    public OrderController(ISender sender) : base(sender) { }

    public Task<Order?> CreateOrder(string number,
                                            string name,
                                            int supplierId,
                                            int vendorId,
                                            int customerId,
                                            int statusId,
                                            int priorityId,
                                            string notes) {
        return Sender.Send(new CreateOrder.Command(number, name, supplierId, vendorId, customerId, statusId, priorityId, notes));
    }

    public Task<Order?> CreateOrder(string number, string name) {
        return CreateOrder(number, name, -1, -1, -1, -1, -1, "");
    }

    public Task<IEnumerable<Order>> GetAllOrders() {
        return Sender.Send(new GetAllOrders.Query());
    }

}