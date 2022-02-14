namespace Persistance.Repositories.OrderItems;

public interface IOrderItemRepository {

    public OrderItemDAO CreateItem(int orderId, int productId, string productName, int qty = 1);

    public IEnumerable<OrderItemDAO> GetItemsByOrderId(int orderId);

    public void UpdateItem(OrderItemDAO item);

}