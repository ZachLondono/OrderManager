using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class UpdateProductClass {

    public record Command(int ClassId, string Name) : IRequest<ProductClass>;

    public class Handler : IRequestHandler<Command, ProductClass> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductClass> Handle(Command request, CancellationToken cancellationToken) {
            
            const string command = @"UPDATE [Catalog].[ProductClasses]
                                    SET [Name] = @Name
                                    WHERE [Id] = @ClassId;";

            int rows = await _connection.QuerySingleAsync<int>(command, new { request.Name, request.ClassId });

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
