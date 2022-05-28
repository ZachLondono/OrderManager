using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class CreateProductClass {

    public record Command(string Name) : IRequest<ProductClass>;

    public class Handler : IRequestHandler<Command, ProductClass> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductClass> Handle(Command request, CancellationToken cancellationToken) {

            const string command = @"INSERT INTO [Catalog].[ProductClasses]
                                    ([Name])
                                    VALUES (@Name);
                                    SELECT SCOPE_IDENTITY();";

            int newId = await _connection.QuerySingleAsync<int>(command, new { request.Name });

            return new() {
                Id = newId,
                Name = request.Name
            };

        }

    }

}