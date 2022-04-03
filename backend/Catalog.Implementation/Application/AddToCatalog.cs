using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class AddToCatalog {

    public record Command(string Name) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {
            const string query = @"INSERT INTO [Catalog].[Products] ([Name]) VALUES (@Name);
                                SELECT SCOPE_IDENTITY();";

            int newId = await _connection.QuerySingleAsync<int>(query, request);

            return newId;
        }
    }

}