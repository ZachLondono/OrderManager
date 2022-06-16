using MediatR;
using Dapper;
using Sales.Implementation.Domain;
using FluentValidation;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

public class PlaceOrder {

    // TODO Info field should be a dictionary
    public record Command(string Name, string Number, int CustomerId, int VendorId, int SupplierId, string Info) : IRequest<int>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid order name");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid order number");

            RuleFor(x => x.CustomerId)
                .NotEqual(0)
                .WithMessage("Invalid customer id");

            RuleFor(x => x.VendorId)
                .NotEqual(0)
                .WithMessage("Invalid vendor id");

            RuleFor(x => x.SupplierId)
                .NotEqual(0)
                .WithMessage("Invalid suppler id");


        }

    }

    public class Handler : IRequestHandler<Command, int> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"INSERT INTO [Sales].[Orders] ([Name], [Number], [CustomerId], [VendorId], [SupplierId], [Info], [Status], [PlacedDate])
                                            VALUES (@Name, @Number, @CustomerId, @VendorId, @SupplierId, @Fields, @Status, @PlacedDate);",

                PersistanceMode.SQLite => @"INSERT INTO [Orders] ([Name], [Number], [CustomerId], [VendorId], [SupplierId], [Info], [Status], [PlacedDate])
                                            VALUES (@Name, @Number, @CustomerId, @VendorId, @SupplierId, @Fields, @Status, @PlacedDate);",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            int newId = await _settings.Connection.QuerySingleAsync<int>(command, new {
                request.Name,
                request.Number,
                request.CustomerId,
                request.VendorId,
                request.SupplierId,
                request.Info,
                Status = OrderStatus.Bid.ToString(),
                PlacedDate = DateTime.Now,
            });

            return newId;

        }
    }

}