using Catalog.Contracts;
using Dapper;
using MediatR;

namespace Catalog.Implementation.Application;

public class CreateProductClass {

    public record Command(string Name) : IRequest<ProductClass>;

    public class Handler : IRequestHandler<Command, ProductClass> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<ProductClass> Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => @"INSERT INTO [Catalog].[ProductClasses]
                                            ([Name])
                                            VALUES (@Name)
                                            RETURNING [Id];",
                
                PersistanceMode.SQLite => @"INSERT INTO [ProductClasses]
                                            ([Name])
                                            VALUES (@Name)
                                            RETURNING [Id];",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };

            int newId = await _settings.Connection.QuerySingleAsync<int>(command, new { request.Name });

            return new() {
                Id = newId,
                Name = request.Name
            };

        }

    }

}