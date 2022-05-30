using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class GetProductClass {

    public record Query(int ProductId) : IRequest<ProductClass>;

    public class Handler : IRequestHandler<Query, ProductClass> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductClass> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT [Class]
                                    FROM [Catalog].[Products]
                                    WHERE [Id] = @ProductId;";

            var prodClass = await _connection.QuerySingleAsync<ProductClass>(query, request);

            return prodClass;

        }

    }

}