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
        public Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            //return Task.FromResult(Guid.NewGuid());
            throw new NotImplementedException();
        }
    }

}