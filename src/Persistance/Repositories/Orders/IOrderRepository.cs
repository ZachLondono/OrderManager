using Persistance.Repositories.Orders;

namespace Persistance.Repositories.Orders;

public interface IOrderRepository {

    public OrderDAO GetOrderById(int id);

    public IEnumerable<OrderDAO> GetOrders();

    public OrderDAO CreateOrder(string Number);

    public void UpdateOrder(OrderDAO order);

}
