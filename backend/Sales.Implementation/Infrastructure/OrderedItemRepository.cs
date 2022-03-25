using System.Data;

namespace Sales.Implementation.Infrastructure;

public class OrderedItemRepository {

    private readonly IDbConnection _connection;

    public OrderedItemRepository(IDbConnection connection) {
        _connection = connection;
    }

    public Task<OrderedItemContext> GetItemById(Guid Id) => throw new NotImplementedException();

    public Task Save(OrderedItemContext item) => throw new NotImplementedException();

}