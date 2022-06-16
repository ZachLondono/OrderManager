using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.OrderedItems;

public class GetOrderedItemsDetails {

    public record Query(int OrderId) : IRequest<IEnumerable<OrderedItemDetails>>;

    public class Handler : IRequestHandler<Query, IEnumerable<OrderedItemDetails>> {


        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderedItemDetails>> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [ProductName], [ProductId], [ProductClass], [Quantity], [Options]
                                                FROM [Sales].[OrderedItems]
                                                WHERE [OrderId] = @OrderId;",

                PersistanceMode.SQLite => @"SELECT [Id], [ProductName], [ProductId], [ProductClass], [Quantity], [Options]
                                            FROM [OrderedItems]
                                            WHERE [OrderId] = @OrderId;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var items = await _settings.Connection.QueryAsync<OrderedItemDetails>(query, request);

            return items;

        }

    }

}