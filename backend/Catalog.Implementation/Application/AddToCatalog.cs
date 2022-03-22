using MediatR;

namespace Catalog.Implementation.Application;

internal class AddToCatalog {

    public record Command(string Name) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}