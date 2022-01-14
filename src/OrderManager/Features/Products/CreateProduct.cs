using MediatR;
using FluentValidation;
using OrderManager.ApplicationCore.Domain;
using Microsoft.Extensions.Logging;

namespace OrderManager.ApplicationCore.Features.Products;

public class CreateProduct { 
    
    public record Command(string Name, string Description, IEnumerable<string> Attributes) : IRequest<Product>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product Name must not be empty");
            
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product Description must not be empty");
            
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