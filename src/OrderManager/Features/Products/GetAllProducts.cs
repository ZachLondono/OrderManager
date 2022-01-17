using MediatR;
using OrderManager.ApplicationCore.Domain;
using System.Data.OleDb;
using OrderManager.ApplicationCore.Infrastructure;
using Dapper;

namespace OrderManager.ApplicationCore.Features.Products;

public class GetAllProducts {

    public record Query() : IRequest<IEnumerable<Product>>;

    public class Handler : IRequestHandler<Query, IEnumerable<Product>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Product>> Handle(Query request, CancellationToken cancellationToken) {

            string query = "SELECT * FROM [Products];";

            using var connection = new OleDbConnection(_config.ConnectionString);

            IEnumerable<Product> products = Enumerable.Empty<Product>();

            connection.Open();

            products = await connection.QueryAsync<Product>(query);

            connection.Close();

            return products;

        }
    }
}
