using Catalog.Contracts;
using Dapper;
using MediatR;

namespace Catalog.Implementation.Application;

public class GetProductClass {

    public record Query(int ProductId) : IRequest<ProductClass>;

    public class Handler : IRequestHandler<Query, ProductClass> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<ProductClass> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Class]
                                                FROM [Catalog].[Products]
                                                WHERE [Id] = @ProductId;",

                PersistanceMode.SQLite => @"SELECT [Class]
                                            FROM [Products]
                                            WHERE [Id] = @ProductId;",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };


            var prodClass = await _settings.Connection.QuerySingleAsync<ProductClass>(query, request);

            return prodClass;

        }

    }

}