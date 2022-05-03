using MediatR;
using System.Data;
using Dapper;
using FluentValidation;

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

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            const string command = @"INSERT INTO [Sales].[Companies] ([Name]) VALUES (@Name);
                                    SELECT SCOPE_IDENTITY();";

            int newId = await _connection.QuerySingleAsync<int>(command, new {
                request.Name
            });

            return newId;

        }
    }

}