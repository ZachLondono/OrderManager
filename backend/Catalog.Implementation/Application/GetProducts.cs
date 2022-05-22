using Catalog.Contracts;
using MediatR;
using Dapper;
using System.Data;

namespace Catalog.Implementation.Application;

public class GetProducts {

    public record Query() : IRequest<IEnumerable<ProductSummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ProductSummary>> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<ProductSummary>> Handle(Query request, CancellationToken cancellationToken) {
            
            const string query = "SELECT [Id], [Name] FROM [Catalog].[Products]";

            var products = await _connection.QueryAsync<ProductSummary>(query);

            return products;

        }
    }

}