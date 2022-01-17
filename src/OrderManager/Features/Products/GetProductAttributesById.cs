using MediatR;
using System.Data.OleDb;
using OrderManager.ApplicationCore.Infrastructure;
using Dapper;

namespace OrderManager.ApplicationCore.Features.Products;

public class GetProductAttributesById {

    public record Query(int ProductId) : IRequest<IEnumerable<string>>;

    public class Handler : IRequestHandler<Query, IEnumerable<string>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken) {

            string query = "SELECT [AttributeName] FROM [ProductAttributes] WHERE ProductId = @ProductId;";

            using var connection = new OleDbConnection(_config.ConnectionString);

            IEnumerable<string> attributes = Enumerable.Empty<string>();

            connection.Open();

            attributes = await connection.QueryAsync<string>(query, request);

            connection.Close();

            return attributes;

        }

    }

}
