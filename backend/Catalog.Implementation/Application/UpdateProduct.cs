using Catalog.Implementation.Domain;
using Dapper;
using FluentValidation;
using MediatR;
using System.Data;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class UpdateProduct {

    public record Command(int Id, string? Name, Dictionary<string, string>? Attributes) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            const string nameCommand = @"UPDATE [Catalog].[Products]
                                        SET [Name] = @Name
                                        WHERE [Id] = @Id;";

            const string attriubteCommand = @"UPDATE [Catalog].[Products]
                                            SET [Attributes] = @Attributes
                                            WHERE [Id] = @Id;";

            string? command = null;

            string attributesJson = "{}";
            if (request.Attributes is not null) {
                attributesJson = JsonSerializer.Serialize(request.Attributes);
                command += attriubteCommand;
            }

            if (request.Name is not null) {
                command += nameCommand;
            }

            if (command is null) return;

            await _connection.ExecuteAsync(command, new {
                request.Name,
                Attributes = attributesJson
            });

        }
    }

}