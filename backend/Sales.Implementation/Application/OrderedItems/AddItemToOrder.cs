using MediatR;
using Dapper;
using FluentValidation;
using Catalog.Contracts;
using Sales.Contracts;
using PersistanceMode = Sales.Contracts.PersistanceMode;

namespace Sales.Implementation.Application.OrderedItems;

public class AddItemToOrder {

    //TODO options should be a dictionary 
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

        private readonly SalesSettings _settings;
        private readonly CatalogProducts.GetProductClass _getProductClass;

        public Handler(SalesSettings settings, CatalogProducts.GetProductClass getProductClass) {
            _settings = settings;
            _getProductClass = getProductClass;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"INSERT INTO [Sales].[OrderedItems]
                                            ([OrderId], [ProductId], [ProductClass], [ProductName], [Qty], [Options])
                                            VALUES (@OrderId, @ProductId, @ProductName, @Qty, @Options);
                                            SELECT SCOPE_IDENTITY();",

                PersistanceMode.SQLite => @"INSERT INTO [Sales].[OrderedItems]
                                            ([OrderId], [ProductId], [ProductClass], [ProductName], [Qty], [Options])
                                            VALUES (@OrderId, @ProductId, @ProductName, @Qty, @Options)
                                            RETURNING [Id];",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var prodClass = await _getProductClass(request.ProductId);

            int newId = await _settings.Connection.QuerySingleAsync<int>(command, new {
                request.OrderId,
                request.ProductId,
                ProductClass = prodClass.Id,
                request.ProductName,
                request.Qty,
                request.Options
            });

            return newId;

        }
    }
}