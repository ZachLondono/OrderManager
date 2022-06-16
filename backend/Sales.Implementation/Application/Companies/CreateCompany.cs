using MediatR;
using Dapper;
using FluentValidation;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

public class CreateCompany {

    public record Command(string Name) : IRequest<int>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid company name");

        }

    }

    public class Handler : IRequestHandler<Command, int> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"INSERT INTO [Sales].[Companies] ([Name]) VALUES (@Name); SELECT SCOPE_IDENTITY();",
                
                PersistanceMode.SQLite => @"INSERT INTO [Companies] ([Name]) VALUES (@Name) RETURNING [Id];",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            int newId = await _settings.Connection.QuerySingleAsync<int>(command, new {
                request.Name
            });

            return newId;

        }
    }

}