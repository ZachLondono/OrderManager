using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class GetProductDetails {

    public record Query(int ProductId) : IRequest<ProductDetails>;

    public class Handler : IRequestHandler<Query, ProductDetails> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductDetails> Handle(Query request, CancellationToken cancellationToken) {
            
            const string query = "SELECT [Id], [Name] FROM [Products] WHERE [Id] = @Id;";
            const string attrQuery = "SELECT [Option] FROM [Attributes] WHERE [ProductId] = @ProductId;";

            var product = await _connection.QuerySingleAsync<ProductDetails>(query, new { Id = request.ProductId });
            var attributes = await _connection.QueryAsync<string>(attrQuery, new { request.ProductId });
            
            product.Attributes = attributes.ToArray();

            return product;

        }
    }

}
