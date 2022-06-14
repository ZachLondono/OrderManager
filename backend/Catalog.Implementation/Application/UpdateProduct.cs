using Catalog.Contracts;
using Dapper;
using MediatR;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class UpdateProduct {

    public record Command(int Id, string? Name, int? Class, Dictionary<string, string>? Attributes) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            string nameCommand = NameCommand();

            string classCommand = ClassCommand();

            string attriubteCommand = AttributeCommand();

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

            await _settings.Connection.ExecuteAsync(command, new {
                request.Name,
                Attributes = attributesJson
            });

        }

        public string NameCommand() => _settings.PersistanceMode switch {
            
            PersistanceMode.SQLServer => @"UPDATE [Catalog].[Products]
                                            SET [Name] = @Name
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [Products]
                                        SET [Name] = @Name
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode"),
        };

        public string ClassCommand() => _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Catalog].[Products]
                                            SET [Class] = @Class
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [Products]
                                        SET [Class] = @Class
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode"),
        };

        public string AttributeCommand() => _settings.PersistanceMode switch {
            
            PersistanceMode.SQLServer => @"UPDATE [Catalog].[Products]
                                            SET [Attributes] = @Attributes
                                            WHERE [Id] = @Id;",
            
            PersistanceMode.SQLite => @"UPDATE [Products]
                                        SET [Attributes] = @Attributes
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode"),
        };

    }

}