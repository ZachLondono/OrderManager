using Catalog.Implementation.Infrastructure;
using FluentValidation;
using MediatR;

namespace Catalog.Implementation.Application;

public class AddAttributeToProduct {

    public record Command(Guid ProductId, string Name) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.ProductId)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotEmpty();

            // TODO: make sure product with given Id exists
            // TODO: make sure product does not yet have an attribute with given name

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var product = await _repository.GetProductById(request.ProductId);

            product.AddAttribute(request.Name);

        }
    }

}
