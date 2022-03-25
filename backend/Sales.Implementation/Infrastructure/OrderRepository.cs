using System.Data;

namespace Sales.Implementation.Infrastructure;

public class OrderRepository {

    private readonly IDbConnection _connection;

    public OrderRepository(IDbConnection connection) {
        _connection = connection;
    }

    public Task<OrderContext> GetOrderById(Guid id) => throw new NotImplementedException();

    public Task Save(OrderContext order) => throw new NotImplementedException();

}