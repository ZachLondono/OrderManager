using MediatR;

namespace Catalog.Implementation.Application;

public class SetProductName {

    public record Command(Guid ProductId, string Name) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
