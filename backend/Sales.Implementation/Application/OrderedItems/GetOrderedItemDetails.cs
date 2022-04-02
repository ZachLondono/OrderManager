using System.Data;
using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.OrderedItems;

public class GetOrderedItemsDetails {

    public record Query(int OrderId) : IRequest<IEnumerable<OrderedItemDetails>>;

    public class Handler : IRequestHandler<Query, IEnumerable<OrderedItemDetails>> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<OrderedItemDetails>> Handle(Query request, CancellationToken cancellationToken) {

            const string query = "SELECT [Id], [ProductName], [ProductId], [Quantity], [Options] FROM [OrderedItems] WHERE [OrderId] = @OrderId;";

            var items = await _connection.QueryAsync<OrderedItemDetails>(query, request);

            return items;

        }

    }

}