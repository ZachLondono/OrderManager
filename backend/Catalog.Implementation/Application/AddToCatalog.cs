using Catalog.Contracts;
using Dapper;
using FluentValidation;
using MediatR;
using System.Data;
using System.Text.Json;

namespace Catalog.Implementation.Application;

public class AddToCatalog {

    public record Command(string Name, Dictionary<string,string>? Attributes) : IRequest<ProductDetails>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid product name");

        }

    }

    public class Handler : IRequestHandler<Command, ProductDetails> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<ProductDetails> Handle(Command request, CancellationToken cancellationToken) {

            const string query = @"INSERT INTO [Catalog].[Products] ([Name], [Attributes]) VALUES (@Name, @Attributes);
                                SELECT SCOPE_IDENTITY();";

            string attributeJson = "{}";
            if (request.Attributes is not null) {
                attributeJson = JsonSerializer.Serialize(request.Attributes);
            }

            int newId = await _connection.QuerySingleAsync<int>(query, new {
                request.Name,
                Attributes = attributeJson
            });

            var product = new ProductDetails() {
                Id = newId,
                Name = request.Name,
                Attributes = request.Attributes ?? new()
            };

            return product;

        }
    }

}