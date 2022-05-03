using System.Data;
using MediatR;
using Dapper;
using FluentValidation;

namespace Sales.Implementation.Application.OrderedItems;

public class AddItemToOrder {

    public record Command(int OrderId, int ProductId, string ProductName, int Qty, string Options) : IRequest<int>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.OrderId)
                .NotEqual(0)
                .WithMessage("Invalid order id");

            RuleFor(x => x.ProductId)
                .NotEqual(0)
                .WithMessage("Invalid product id");

            RuleFor(x => x.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid product name");

            RuleFor(x => x.Qty)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Invalid product quantity");

        }

    }

    public class Handler : IRequestHandler<Command, int> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            const string command = @"INSERT INTO [Sales].[OrderedItems]
                                ([OrderId], [ProductId], [ProductName], [Qty], [Options])
                                VALUES (@OrderId, @ProductId, @ProductName, @Qty, @Options);
                                SELECT SCOPE_IDENTITY();";

            int newId = await _connection.QuerySingleAsync<int>(command, request);

            return newId;

        }
    }
}