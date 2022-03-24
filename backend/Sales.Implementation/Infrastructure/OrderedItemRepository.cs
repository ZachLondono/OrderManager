using System.Data;

namespace Sales.Implementation.Infrastructure;

internal class OrderedItemRepository {

    private readonly IDbConnection _connection;

    public OrderedItemRepository(IDbConnection connection) {
        _connection = connection;
    }

    public void GetItemById(Guid Id) => throw new NotImplementedException();

    public void Save(OrderedItemContext item) => throw new NotImplementedException();

}