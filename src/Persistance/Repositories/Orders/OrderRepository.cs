namespace Persistance.Repositories.Orders;

public class OrderRepository : BaseRepository, IOrderRepository {
    
    public OrderRepository(string connectionString) : base(connectionString) {
    }

    public OrderDAO CreateOrder(string number) {

        const string sql = @"INSERT INTO [Orders] ([Number])
                            VALUES ([@Number])
                            RETURNING Id;";

        int newId = QuerySingleOrDefault<int>(sql, new { Number = number });

        return new() {
            Id = newId,
            Number = number
        };

    }

    public OrderDAO GetOrderById(int id) {

        const string sql = @"SELECT ([Id], [Number]) FROM [Orders] WHERE Id = @Id;";

        OrderDAO? dao = QuerySingleOrDefault<OrderDAO>(sql, new { Id = id });

        if (dao is null)
            throw new InvalidDataException($"Order with id '{id}' was not found");
        
        return dao;

    }

    public IEnumerable<OrderDAO> GetOrders() {
        const string sql = @"SELECT ([Id], [Number]) FROM [Orders];";
        return Query<OrderDAO>(sql);
    }

    public void UpdateOrder(OrderDAO order) {
        const string sql = @"UPDATE [Orders]
                            SET [Number] = [@Number]
                            WHERE [Id] = @Id;";
        Execute(sql, order);
    }
}
