using Catalog.Contracts;
using Dapper;
using FluentValidation;
using MediatR;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class AddToCatalog {

    public record Command(string Name, int Class, Dictionary<string,string>? Attributes) : IRequest<ProductDetails>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid product name");

        }

    }

    public class Handler : IRequestHandler<Command, ProductDetails> {

        private readonly CatalogSettings _settings;

        public Handler(CatalogSettings settings) {
            _settings = settings;
        }

        public async Task<ProductDetails> Handle(Command request, CancellationToken cancellationToken) {
            
            string query = _settings.PersistanceMode switch {
                
                PersistanceMode.SQLServer => @"INSERT INTO [Catalog].[Products]
                                                ([Name], [Class], [Attributes])
                                                VALUES (@Name, [@Class], @Attributes);
                                                SELECT SCOPE_IDENTITY();",


                PersistanceMode.SQLite => @"INSERT INTO [Products]
                                            ([Name], [Class], [Attributes])
                                            VALUES (@Name, [@Class], @Attributes)
                                            RETURNING [Id];",


                _ => throw new InvalidDataException("Invalid DataBase mode"),
            };

            string attributeJson = "{}";
            if (request.Attributes is not null) {
                attributeJson = JsonSerializer.Serialize(request.Attributes);
            }

            int newId = await _settings.Connection.QuerySingleAsync<int>(query, new {
                request.Name,
                request.Class,
                Attributes = attributeJson
            });

            var product = new ProductDetails() {
                Id = newId,
                Name = request.Name,
                Class = request.Class,
                Attributes = request.Attributes ?? new()
            };

            return product;

        }
    }

}