using Catalog.Contracts;
using Dapper;
using MediatR;

namespace Catalog.Implementation.Application;

public class UpdateProductClass {

    public record Command(int ClassId, string Name) : IRequest<ProductClass>;

    public class Handler : IRequestHandler<Command, ProductClass> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<ProductClass> Handle(Command request, CancellationToken cancellationToken) {
            
            string command = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => @"UPDATE [Catalog].[ProductClasses]
                                                SET [Name] = @Name
                                                WHERE [Id] = @ClassId;",
                
                PersistanceMode.SQLite => @"UPDATE [ProductClasses]
                                            SET [Name] = @Name
                                            WHERE [Id] = @ClassId;",

                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };

            int rows = await _settings.Connection.QuerySingleAsync<int>(command, new { request.Name, request.ClassId });

            if (rows > 0) { 
                
                return new() {
                    Id = request.ClassId,
                    Name = request.Name
                };

            } else {
                throw new InvalidDataException("Could not update data");
            }

        }

    }

}
