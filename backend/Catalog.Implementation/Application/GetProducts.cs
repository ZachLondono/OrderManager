using Catalog.Contracts;
using MediatR;
using Dapper;

namespace Catalog.Implementation.Application;

public class GetProducts {

    public record Query() : IRequest<IEnumerable<ProductSummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ProductSummary>> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<IEnumerable<ProductSummary>> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => "SELECT [Id], [Name] FROM [Catalog].[Products]",
                
                PersistanceMode.SQLite => "SELECT [Id], [Name] FROM [Products]",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };

            var products = await _settings.Connection.QueryAsync<ProductSummary>(query);

            return products;

        }
    }

}