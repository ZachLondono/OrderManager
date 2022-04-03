using System.Data;
using MediatR;
using Dapper;

namespace Sales.Implementation.Application.OrderedItems;

public class AddItemToOrder {

    public record Command(int OrderId, int ProductId, string ProductName, int Qty, string Options) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            const string command = @"INSERT INTO [Sales].[OrderedItems]
                                ([OrderId], [ProductId], [ProductName], [Qty], [Options])
                                VALUES (@OrderId, @ProductId, @ProductName, @Qty, @Options);
                                SELECT SCOPE_IDENTITY();";

            int newId = await _connection.QuerySingleAsync<int>(command, request);

            return newId;

        }
    }
}