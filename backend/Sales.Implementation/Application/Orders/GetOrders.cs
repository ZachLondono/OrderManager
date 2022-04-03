using Dapper;
using MediatR;
using Sales.Contracts;
using System.Data;

namespace Sales.Implementation.Application.Orders;

public class GetOrders {

    public record Query() : IRequest<IEnumerable<OrderSummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<OrderSummary>> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<OrderSummary>> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT [Id], [Name], [Number], [CustomerId], [PlacedDate], [Status]
                                    FROM [Sales].[Orders];";

            var orders = await _connection.QueryAsync<OrderSummary>(query);

            return orders;

        }
    }

}
