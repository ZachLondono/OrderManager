using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class GetProductClasses {

    public record Query() : IRequest<IEnumerable<ProductClass>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ProductClass>> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<ProductClass>> Handle(Query request, CancellationToken cancellationToken) {

            const string command = @"SELECT (Id], [Name])
                                    FROM [Catalog].[ProductClasses];";

            var classes = await _connection.QueryAsync<ProductClass>(command);

            return classes;

        }

    }

}
