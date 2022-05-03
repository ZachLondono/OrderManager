using MediatR;
using System.Data;
using Dapper;
using Sales.Implementation.Domain;
using FluentValidation;

namespace Sales.Implementation.Application.Orders;

public class PlaceOrder {

    public record Command(string Name, string Number, int CustomerId, int VendorId, int SupplierId, string Fields) : IRequest<int>;

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

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            const string command = @"INSERT INTO [Sales].[Orders] ([Name], [Number], [CustomerId], [VendorId], [SupplierId], [Fields], [Status], [PlacedDate])
                                    VALUES (@Name, @Number, @CustomerId, @VendorId, @SupplierId, @Fields, @Status, @PlacedDate);";

            int newId = await _connection.QuerySingleAsync<int>(command, new {
                request.Name,
                request.Number,
                request.CustomerId,
                request.VendorId,
                request.SupplierId,
                request.Fields,
                Status = OrderStatus.Bid.ToString(),
                PlacedDate = DateTime.Now,
            });

            return newId;

        }
    }

}