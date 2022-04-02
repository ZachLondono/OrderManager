using System.Data;
using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

public class GetOrderDetails {

    public record Query(int Id) : IRequest<OrderDetails>;

    public class Handler : IRequestHandler<Query, OrderDetails> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<OrderDetails> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT [Id], [Name], [Number], [CustomerId], [VendorId], [SupplierId], [Status], [PlacedDate], [ConfirmationDate], [CompletionDate], [CanceldDate], [Fields]
                                    FROM [Orders]
                                    WHERE [Id] = @Id;";

            var order = await _connection.QuerySingleAsync<OrderDetails>(query, request);

            const string itemQuery = @"SELECT [Id] FROM [OrderedItems] WHERE [OrderId] = @OrderId";

            var items = await _connection.QueryAsync<int>(itemQuery, new { OrderId = request.Id });
            order.OrderedItems = items;

            return order;
            
        }
    }

}