using MediatR;
using Dapper;
using System.Data;

namespace Catalog.Implementation.Application;

public class RemoveFromCatalog {

    public record Command(int ProductId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            
            const string command = "DELETE FROM [Catalog].[Products] WHERE [Id] = @ProductId";
            
            await _connection.ExecuteAsync(command, new { request.ProductId });

        }
    }

}