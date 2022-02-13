namespace Persistance.Repositories.OrderItems;

public interface IOrderItemRepository {

    public OrderItemDAO CreateItem(int orderId, int productId, int qty = 1);

    public IEnumerable<OrderItemDAO> GetItemsByOrderId(int orderId);

    public void UpdateItem(OrderItemDAO item);

}

public class OrderItemRepository : BaseRepository, IOrderItemRepository {
    
    public OrderItemRepository(string connectionString) : base(connectionString) {
    }

    public OrderItemDAO CreateItem(int orderId, int productId, int qty = 1) {
        const string sql = @"INSERT INTO [OrderItems] ([OrderId], [ProductId], [Qty])
                            VALUES (@OrderId, @ProductId, @Qty)
                            RETURNING Id;";
        int newId = QuerySingleOrDefault<int>(sql, new { OrderId = orderId, ProductId = productId, Qty = qty });
        return new() {
            Id = newId,
            OrderId = orderId,
            ProductId = productId,
            Qty = qty
        };
    }

    public IEnumerable<OrderItemDAO> GetItemsByOrderId(int orderId) {
        const string query = @"SELECT ([Id], [OrderId], [ProductId], [Qty]) FROM [OrderItems] WHERE [OrderId] = @OrderId;";
        return Query<OrderItemDAO>(query, new { OrderId = orderId });
    }

    public void UpdateItem(OrderItemDAO item) {
        const string sql = @"UPDATE [OrderItems]
                            SET ([OrderId] = @OrderId, [ProductId] = @ProductId, [Qty] = @Qty)
                            WHERE [Id] = @Id;";
        Execute(sql, item);
    }

}