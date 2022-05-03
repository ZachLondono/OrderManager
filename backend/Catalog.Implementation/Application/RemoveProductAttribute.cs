using Catalog.Implementation.Infrastructure;
using FluentValidation;
using MediatR;

namespace Catalog.Implementation.Application;

public class RemoveProductAttribute {

    public record Command(int ProductId, string Attribute) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.ProductId).NotEqual(0).WithMessage("Invalid Product Id");
            RuleFor(x => x.Attribute).NotNull().WithMessage("Invalid Product Attribute");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var product = await _repository.GetProductById(request.ProductId);

            product.RemoveAttribute(request.Attribute);

            await _repository.Save(product);

        }
    }

}
