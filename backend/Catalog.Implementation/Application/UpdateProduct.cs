using Catalog.Implementation.Domain;
using Dapper;
using FluentValidation;
using MediatR;
using System.Data;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class UpdateProduct {

    public record Command(int Id, string? Name, int? Class, Dictionary<string, string>? Attributes) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            const string nameCommand = @"UPDATE [Catalog].[Products]
                                        SET [Name] = @Name
                                        WHERE [Id] = @Id;";

            const string classCommand = @"UPDATE [Catalog].[Products]
                                        SET [Class] = @Class
                                        WHERE [Id] = @Id;";

            const string attriubteCommand = @"UPDATE [Catalog].[Products]
                                            SET [Attributes] = @Attributes
                                            WHERE [Id] = @Id;";

            string command = string.Empty;

            string attributesJson = "{}";
            if (request.Attributes is not null) {
                attributesJson = JsonSerializer.Serialize(request.Attributes);
                command += attriubteCommand;
            }

            if (request.Name is not null) {
                command += nameCommand;
            }

            if (request.Class is not null) {
                command += classCommand;
            }

            if (string.IsNullOrEmpty(command)) return;

            await _connection.ExecuteAsync(command, new {
                request.Name,
                Attributes = attributesJson
            });

        }
    }

}