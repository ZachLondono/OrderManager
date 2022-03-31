using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class GetProducts {

    public record Query() : IRequest<ProductSummary[]>;

    public class Handler : IRequestHandler<Query, ProductSummary[]> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductSummary[]> Handle(Query request, CancellationToken cancellationToken) {
            var products = await _connection.QueryAsync<ProductSummary>("SELECT [Id], [Name] FROM [Products];");
            return products.ToArray();
        }
    }

}