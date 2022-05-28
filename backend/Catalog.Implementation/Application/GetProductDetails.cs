using Catalog.Contracts;
using Catalog.Implementation.Infrastructure.Persistance;
using Dapper;
using MediatR;
using System.Data;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class GetProductDetails {

    public record Query(int ProductId) : IRequest<ProductDetails>;

    public class Handler : IRequestHandler<Query, ProductDetails> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductDetails> Handle(Query request, CancellationToken cancellationToken) {

            const string query = "SELECT [Id], [Name], [Class], [Attributes] FROM [Catalog].[Products] WHERE [Id] = @Id;";

            var productDto = await _connection.QuerySingleAsync<Product>(query, new { Id = request.ProductId });

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
