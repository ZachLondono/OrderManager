using Catalog.Contracts;
using Dapper;
using MediatR;

namespace Catalog.Implementation.Application;

public class GetProductClasses {

    public record Query() : IRequest<IEnumerable<ProductClass>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ProductClass>> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<IEnumerable<ProductClass>> Handle(Query request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => "SELECT [Id], [Name] FROM [Catalog].[ProductClasses];",
                
                PersistanceMode.SQLite => "SELECT [Id], [Name] FROM [ProductClasses];",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };
            
            var classes = await _settings.Connection.QueryAsync<ProductClass>(command);

            return classes;

        }

    }

}