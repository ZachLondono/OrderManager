using System.Data;

namespace Sales.Implementation.Infrastructure;

internal class OrderRepository {

    private readonly IDbConnection _connection;

    public OrderRepository(IDbConnection connection) {
        _connection = connection;
    }

    public OrderContext GetOrderById(Guid id) => throw new NotImplementedException();

    public void Save(OrderContext order) => throw new NotImplementedException();

}