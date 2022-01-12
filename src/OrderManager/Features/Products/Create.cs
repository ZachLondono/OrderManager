using MediatR;
using FluentValidation;
using OrderManagment.Models;
using Microsoft.Extensions.Logging;

namespace OrderManagment.Features.Products;

public class Create { 
    
    internal record Command(string Name, string Description, IEnumerable<string> Attributes) : IRequest<Product>;

    internal class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product Name must not be empty");
            
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product Description must not be null");
            
            RuleFor(p => p.Attributes)
                .NotNull()
                .WithMessage("Product Attributes must not be null");
        }
    }

    internal class Handler : IRequestHandler<Command, Product> {

        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger) {
            _logger = logger;
        }

        public Task<Product> Handle(Command request, CancellationToken cancellationToken) {
            _logger.LogInformation("Creating product with parameters: {Name} {Description} {Attributes}", request.Name, request.Description, request.Attributes);
            throw new NotImplementedException();
        }

    }
}