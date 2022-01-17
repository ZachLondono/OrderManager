using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Scripts;

public class CreateScript {

    public record Command(string Name, string Source) : IRequest<Script>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            // TODO: check that the script name does not exist yet
            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Source)
                .NotNull()
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Script> {
        public Task<Script> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}