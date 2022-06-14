using Catalog.Contracts;
using Catalog.Implementation.Infrastructure.Persistance;
using Dapper;
using MediatR;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class GetProductDetails {

    public record Query(int ProductId) : IRequest<ProductDetails>;

    public class Handler : IRequestHandler<Query, ProductDetails> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<ProductDetails> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => "SELECT [Id], [Name], [Class], [Attributes] FROM [Catalog].[Products] WHERE [Id] = @Id;",

                PersistanceMode.SQLite => "SELECT [Id], [Name], [Class], [Attributes] FROM [Products] WHERE [Id] = @Id;",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };

            var productDto = await _settings.Connection.QuerySingleAsync<Product>(query, new { Id = request.ProductId });

            var attributes = JsonSerializer.Deserialize<Dictionary<string, string>>(productDto.Attributes);

            var product = new ProductDetails() {
                Id = productDto.Id,
                Name = productDto.Name,
                Attributes = attributes ?? new()
            };

            return product;

        }
    }

}
