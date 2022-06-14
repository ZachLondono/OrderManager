using Catalog.Contracts;
using Dapper;
using MediatR;

namespace Catalog.Implementation.Application;

public class RemoveProductClass {

    public record Command(int ClassId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => "DELETE [Catalog].[ProductClasses] WHERE [Id] = @ClassId;",

                PersistanceMode.SQLite => "DELETE [ProductClasses] WHERE [Id] = @ClassId;",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };

            await _settings.Connection.QuerySingleAsync<int>(command, new { request.ClassId });

        }
    }

}