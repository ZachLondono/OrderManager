using Catalog.Implementation.Infrastructure;
using FluentValidation;
using MediatR;

namespace Catalog.Implementation.Application;

public class AddToCatalog {

    public record Command(string Name) : IRequest<Guid>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Attribute name cannot be empty");

            // TODO: make sure product with given name does not already exist

        }

    }

    public class Handler : IRequestHandler<Command, Guid> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            var product = await _repository.Add(request.Name);
            return product.Id;
        }
    }

}