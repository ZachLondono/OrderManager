using MediatR;
using Dapper;
using Catalog.Contracts;

namespace Catalog.Implementation.Application;

public class RemoveFromCatalog {

    public record Command(int ProductId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => "DELETE FROM [Catalog].[Products] WHERE [Id] = @ProductId",
                
                PersistanceMode.SQLite => "DELETE FROM [Products] WHERE [Id] = @ProductId",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };
            
            await _settings.Connection.ExecuteAsync(command, new { request.ProductId });

        }
    }

}