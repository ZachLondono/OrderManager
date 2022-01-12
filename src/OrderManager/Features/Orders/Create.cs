using MediatR;
using FluentValidation;
using OrderManagment.Models;
using Microsoft.Extensions.Logging;

namespace OrderManagment.Features.Orders;

public class Create {
    
    internal record Command(string RefNum, DateTime OrderDate, IEnumerable<Order.LineItem> LineItems) : IRequest<Order>;

    internal class Validator : AbstractValidator<Command> {
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
