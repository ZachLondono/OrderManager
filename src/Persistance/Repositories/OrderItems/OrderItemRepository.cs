namespace Persistance.Repositories.OrderItems;

public class OrderItemRepository : BaseRepository, IOrderItemRepository {
    
    public OrderItemRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) {
    }

    public OrderItemDAO CreateItem(Guid orderId, int productId, string productName, int qty = 1) {
        const string sql = @"INSERT INTO [OrderItems] ([OrderId], [ProductId], [ProductName], [Qty])
                            VALUES ([@OrderId], [@ProductId], [ProductName], [@Qty])
                            RETURNING Id;";
        int newId = QuerySingleOrDefault<int>(sql, new { OrderId = orderId.ToString(), ProductId = productId, ProductName = productName, Qty = qty });
        return new() {
            Id = newId,
            OrderId = orderId,
            ProductId = productId,
            Qty = qty
        };
    }

    public IEnumerable<OrderItemDAO> GetItemsByOrderId(Guid orderId) {
        // TODO: this can be improved by using dapper result multi-mapping
        const string query = @"SELECT [Id], [OrderId], [ProductId], [ProductName], [Qty] FROM [OrderItems] WHERE [OrderId] = @OrderId;";
        var items =  Query<OrderItemDAO>(query, new { OrderId = orderId.ToString() });

        foreach(var item in items) { 
            const string optionQuery = @"SELECT [Key], [Value] FROM [OrderItemOptions] WHERE [ItemId] = @ItemId;";

            var options = Query<ItemOption>(optionQuery, new { ItemId = item.Id });

            Dictionary<string, string> optionsDict = new();
            foreach (var x in options) {
                if (optionsDict.ContainsKey(x.Key)) continue;
                optionsDict.Add(x.Key, x.Value);
            }

            item.Options = optionsDict;

        }
        return items;
    }

    private class ItemOption {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public void UpdateItem(OrderItemDAO item) {
        const string sql = @"UPDATE [OrderItems]
                            SET [OrderId] = @OrderId, [ProductId] = [@ProductId], [ProductName] = [@ProductName], [Qty] = @Qty
                            WHERE [Id] = @Id;";
        Execute(sql, new {
            OrderId = item.OrderId.ToString(),
            item.ProductId,
            item.ProductName,
            item.Qty,
            item.Id
        });
    }

}