using MediatR;

namespace Manufacturing.Implementation.Application;

internal class ShipJob {

    public record Command(Guid id) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
