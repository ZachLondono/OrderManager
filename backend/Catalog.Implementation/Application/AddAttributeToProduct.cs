using Catalog.Implementation.Infrastructure;
using FluentValidation;
using MediatR;

namespace Catalog.Implementation.Application;

public class AddAttributeToProduct {

    public record Command(int ProductId, string Name, string Default) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.ProductId)
                .NotEqual(0)
                .WithMessage("Invalid product id");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid product attribute name");

            RuleFor(x => x.Default)
                .NotNull()
                .WithMessage("Invalid default attribute value");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var product = await _repository.GetProductById(request.ProductId);

            product.AddAttribute(new() {
                Name = request.Name,
                Default = request.Default
            });

            await _repository.Save(product);

        }
    }

}
