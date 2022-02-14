namespace Persistance.Repositories.OrderItems;

public class OrderItemRepository : BaseRepository, IOrderItemRepository {
    
    public OrderItemRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) {
    }

    public OrderItemDAO CreateItem(int orderId, int productId, string productName, int qty = 1) {
        const string sql = @"INSERT INTO [OrderItems] ([OrderId], [ProductId], [ProductName], [Qty])
                            VALUES ([@OrderId], [@ProductId], [ProductName], [@Qty])
                            RETURNING Id;";
        int newId = QuerySingleOrDefault<int>(sql, new { OrderId = orderId, ProductId = productId, ProductName = productName, Qty = qty });
        return new() {
            Id = newId,
            OrderId = orderId,
            ProductId = productId,
            Qty = qty
        };
    }

    public IEnumerable<OrderItemDAO> GetItemsByOrderId(int orderId) {
        const string query = @"SELECT [Id], [OrderId], [ProductId], [ProductName], [Qty] FROM [OrderItems] WHERE [OrderId] = @OrderId;";
        return Query<OrderItemDAO>(query, new { OrderId = orderId });
    }

    public void UpdateItem(OrderItemDAO item) {
        const string sql = @"UPDATE [OrderItems]
                            SET [OrderId] = @OrderId, [ProductId] = [@ProductId], [ProductName] = [@ProductName], [Qty] = @Qty
                            WHERE [Id] = @Id;";
        Execute(sql, item);
    }

}