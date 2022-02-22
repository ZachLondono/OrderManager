namespace Persistance.Repositories.OrderItems;

public interface IOrderItemRepository {

    public OrderItemDAO CreateItem(Guid orderId, int productId, string productName, int qty = 1);

    public IEnumerable<OrderItemDAO> GetItemsByOrderId(Guid orderId);

    public void UpdateItem(OrderItemDAO item);

}