using MediatR;
using FluentValidation;
using OrderManager.ApplicationCore.Domain;
using Microsoft.Extensions.Logging;

namespace OrderManager.ApplicationCore.Features.Orders;

public class CreateOrder {
    
    public record Command(string RefNum, DateTime OrderDate, IEnumerable<LineItem> LineItems) : IRequest<Order>;

    public class Validator : AbstractValidator<Command> {
        public Validator(ILogger<Validator> logger) {
            RuleFor(p => p.RefNum).NotNull().NotEmpty();
            RuleFor(p => p.OrderDate).NotNull();
            RuleFor(p => p.LineItems).NotNull();
            logger.LogInformation("Parameters for order create command are valid");
        }
    }

    internal class Handler : IRequestHandler<Command, Order> {

        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger) {
            _logger = logger;
        }

        public Task<Order> Handle(Command request, CancellationToken cancellationToken) {
            _logger.LogInformation("Creating order with parameters: {RefNum} {OrderDate} {LineItems}", request.RefNum, request.OrderDate, request.LineItems);
            throw new NotImplementedException();
        }
    }

}
