using Dapper;

namespace Persistance.Repositories.Orders;

public class OrderRepository : BaseRepository, IOrderRepository {
    
    public OrderRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) {
    }

    public OrderDAO CreateOrder(string number) {

        const string sql = @"INSERT INTO [Orders] ([Number])
                            VALUES ([@Number])
                            RETURNING Id;";

        Guid newId = QuerySingleOrDefault<Guid>(sql, new { Number = number });

        return new() {
            Id = newId,
            Number = number
        };

    }

    public OrderDAO GetOrderById(Guid id) {

        const string sql = @"SELECT [Id], [Number], [Name], [LastModified], [IsPriority], [CustomerId], [VendorId], [SupplierId] FROM [Orders] WHERE Id = @Id;";

        OrderDAO? dao = QuerySingleOrDefault<OrderDAO>(sql, new { Id = id.ToString() });

        if (dao is null)
            throw new InvalidDataException($"Order with id '{id}' was not found");
        
        return dao;

    }

    public IEnumerable<OrderDAO> GetOrders() {
        const string sql = @"SELECT [Id], [Number], [Name], [LastModified], [IsPriority], [CustomerId], [VendorId], [SupplierId] FROM [Orders];";
        return Query<OrderDAO>(sql);
    }

    public void UpdateOrder(OrderDAO order) {
        const string sql = @"UPDATE [Orders]
                            SET [Number] = [@Number], [Name] = [@Name], [LastModified] = [@LastModified], [IsPriority] = [@IsPriority], [CustomerId] = [@CustomerId], [VendorId] = [@VendorId], [SupplierId] = [@SupplierId]
                            WHERE [Id] = @Id;";
        Execute(sql, new {
            Id = order.Id.ToString(),
            order.Number,
            order.Name,
            order.LastModified,
            order.IsPriority,
            order.CustomerId,
            order.VendorId,
            order.SupplierId
        });
    }
}
