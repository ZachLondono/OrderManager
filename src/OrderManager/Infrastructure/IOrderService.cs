using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Infrastructure;

public interface IOrderService {

    public IEnumerable<Order> GetNewOrders();

    public Order GetOrder(string refNum);

}
