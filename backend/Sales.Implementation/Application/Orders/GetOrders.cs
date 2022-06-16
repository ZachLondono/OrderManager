using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

public class GetOrders {

    public record Query() : IRequest<IEnumerable<OrderSummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<OrderSummary>> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderSummary>> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [Name], [Number], [Sales].[Companies].[Name] As [Customer], [PlacedDate], [Status]
                                                FROM [Sales].[Orders]
                                                INNER JOIN [Sales].[Companies] ON [Sales].[Orders].CustomerId=[Sales].[Companies].Id;",

                PersistanceMode.SQLite => @"SELECT [Id], [Name], [Number], [Companies].[Name] As [Customer], [PlacedDate], [Status]
                                            FROM [Orders]
                                            INNER JOIN [Companies] ON [Orders].CustomerId=[Companies].Id;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var orders = await _settings.Connection.QueryAsync<OrderSummary>(query);

            return orders;

        }
    }

}
