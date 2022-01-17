using FluentValidation;
using MediatR;

namespace OrderManager.ApplicationCore.Features.Scripts;

public class DeleteScript {

    public record Command(string Name) : IRequest<bool>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, bool> {
        public Task<bool> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
