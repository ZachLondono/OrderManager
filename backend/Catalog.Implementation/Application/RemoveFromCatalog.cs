using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class RemoveFromCatalog {

    public record Command(int Id) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        
        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            const string query = @"DELETE FROM [Catalog].[Products] WHERE [Id] = @Id;";

            int rows = await _connection.ExecuteAsync(query, request);
        }
    }

}